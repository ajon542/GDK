using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using UnityEngine;

public class Client : MonoBehaviour
{
    private void Start()
    {
        Connect();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Send("Client says Hello!");
        }
    }

    /// <summary>
    /// The size of buffer for receiving data.
    /// </summary>
    private const int BufferSize = 1024;

    /// <summary>
    /// The buffer for receiving data.
    /// </summary>
    private byte[] buffer = new byte[BufferSize];

    /// <summary>
    /// The client socket.
    /// </summary>
    private Socket client;

    /// <summary>
    /// Connect to the server.
    /// </summary>
    public void Connect()
    {
        try
        {
            // Establish the remote endpoint for the socket.
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect to the remote endpoint.
            Debug.Log("Attempting to connect to server...");
            client.Connect(remoteEP);

            Debug.Log("Client connected to " + client.RemoteEndPoint);

            // Begin receiving data from the server.
            client.BeginReceive(buffer, 0, BufferSize, 0, ReceiveCallback, null);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    /// <summary>
    /// Send a message to the server.
    /// </summary>
    /// <param name="msg">The message to send.</param>
    public void Send(string msg)
    {
        try
        {
            byte[] data = Encoding.ASCII.GetBytes(msg);
            int bytesSent = client.Send(data);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
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

            Debug.Log("Client Received: " + bytesRead);

            // Copy the data from the buffer.
            byte[] data = new byte[bytesRead];
            Array.Copy(buffer, 0, data, 0, bytesRead);

            // Continue receiving data.
            client.BeginReceive(buffer, 0, BufferSize, 0, ReceiveCallback, null);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    /// <summary>
    /// Close the socket.
    /// </summary>
    public void Close()
    {
        client.Shutdown(SocketShutdown.Both);
        client.Close();
    }
}
