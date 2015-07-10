using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting GDK");

            GameStateMachine stateMachine = new GameStateMachine();

            Game game = new Slot();
            game.ConfigureStates(stateMachine);


            //stateMachine.Run();

            // External input such as comms with the platform and the client
            // will drive the state machine.

            Console.ReadLine();
        }
    }
}
