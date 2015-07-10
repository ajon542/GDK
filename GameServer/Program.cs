using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting GameServer...");

            StateMachine.StateMachine sm = new StateMachine.StateMachine();
            sm.Init();
            sm.Run();

            // External input such as comms with the platform and the client
            // will drive the state machine.

            Console.ReadLine();
        }
    }
}
