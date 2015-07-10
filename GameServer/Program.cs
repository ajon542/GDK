using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    using StateMachine = StateMachine.StateMachine;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting GDK");

            StateMachine stateMachine = new StateMachine();

            Game game = new Slot();
            game.ConfigureStates(stateMachine);



            stateMachine.Init();
            //stateMachine.Run();

            // External input such as comms with the platform and the client
            // will drive the state machine.

            Console.ReadLine();
        }
    }
}
