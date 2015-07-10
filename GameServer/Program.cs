using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MathEngine;
using StateMachine;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example of (de)serializing a paytable.
            //PaytableGenerator paytableGenerator = new PaytableGenerator();
            //byte[] data = paytableGenerator.Serialize();
            //paytableGenerator.Deserialize(data);

            Paytable paytable = new Paytable();
            paytable.ConstructDummyPaytable();

            Evaluator evaluator = new Evaluator();
            evaluator.Evaluate(paytable);



            Console.WriteLine("Starting GDK");

            // Create the state machine.
            GameStateMachine stateMachine = new GameStateMachine();

            // Create and configure the game.
            Game game = new Slot();
            game.ConfigureStates(stateMachine);

            // Example of adding state triggers.
            stateMachine.AddTrigger("TriggerStateIdle");
            stateMachine.AddTrigger("TriggerStatePlay");
            stateMachine.AddTrigger("TriggerStateDrawRandomNumbers");
            stateMachine.AddTrigger("TriggerStateGameOver");
            stateMachine.AddTrigger("TriggerStateIdle");

            // Process the pending triggers and transition to the states.
            while (true)
            {
                stateMachine.ProcessStateTransitions();
                Thread.Sleep(10);
            }
        }
    }
}
