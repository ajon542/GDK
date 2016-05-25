using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientComms
{
    /// <summary>
    /// Basic server implementation.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// The size of buffer for receiving data.
        /// </summary>
        private const int BufferSize = 1024;

        /// <summary>
        /// The buffer for receiving data.
        /// </summary>
        private byte[] buffer = new byte[BufferSize];

        /// <summary>
        /// The listener socket.
        /// </summary>
        private Socket listener;

        /// <summary>
        /// The client socket.
        /// </summary>
        private Socket client;

        /// <summary>
        /// Start listening for a single client connection.
        /// </summary>
        /// <remarks>
        /// Once the connection is made, the server will communicate with the
        /// client until the connection is lost or closed. At this point, it
        /// will begin listening for a new client.
        /// </remarks>
        public void Listen()
        {
            try
            {
                // Establish the local endpoint for the socket.
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP socket.
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Bind to the local endpoint.
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Block until a connection is accepted.
                Console.WriteLine("Waiting for a connection...");
                client = listener.Accept();
                Console.WriteLine("Connection Established.");

                // Begin receiving data from the client.
                client.BeginReceive(buffer, 0, BufferSize, 0, ReceiveCallback, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Send a message to the client.
        /// </summary>
        /// <param name="msg">The message to send.</param>
        public void Send(string msg)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(msg);
                client.Send(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// The asynchronous data received callback.
        /// </summary>
        /// <param name="ar">The asynchronous result.</param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    byte[] data = new byte[bytesRead];
                    Array.Copy(buffer, 0, data, 0, bytesRead);
                    string msg = Encoding.ASCII.GetString(data);
                    Console.WriteLine("Server Received: {0}", msg);

                    // Continue receiving data.
                    client.BeginReceive(buffer, 0, BufferSize, 0, ReceiveCallback, null);
                }
                else
                {
                    // If the client closes the socket normally, bytes read will be 0.
                    // We also want to close the connection and begin listening again.
                    Close();
                    Listen();
                }

            }
            catch (Exception e)
            {
                // This occurs mainly when a client forcibly shuts down
                // the connection. In this case, close the listener and
                // start listening for new connections.
                Console.WriteLine(e);

                Close();
                Listen();
            }
        }

        /// <summary>
        /// Close the socket.
        /// </summary>
        private void Close()
        {
            // TODO: listener.Shutdown causes an exception to be thrown. Not sure why.
            //listener.Shutdown(SocketShutdown.Both);
            listener.Close();
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}
