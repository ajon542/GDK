using System;
using System.Threading;

// TODO: GameServer probably doesn't need reference to all these.
using MathEngine;
using StateMachine;
using GameLibrary;
using SlotLibrary;
using Utilities;

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



            Log.Info("Starting GDK");

            // Create the state machine.
            GameStateMachine stateMachine = new GameStateMachine();

            // Create and configure the game.
            Game game = new Slot();
            game.ConfigureStates(stateMachine);

            // Example of adding state triggers.
            stateMachine.AddTrigger("TriggerStateConfiguration");
            stateMachine.AddTrigger("TriggerStateIdle");
            stateMachine.AddTrigger("TriggerStatePlay");
                stateMachine.AddTrigger("TriggerStateBeginPlay");
                stateMachine.AddTrigger("TriggerStateEvaluate");
            stateMachine.AddTrigger("TriggerStatePayWin");
            stateMachine.AddTrigger("TriggerStateGameOver");
            stateMachine.AddTrigger("TriggerStateIdle");


            Log.Info("hello");
            Log.Warn("hello");
            Log.Fail("hello");
            Log.Info("hello");

            // Process the pending triggers and transition to the states.
            while (true)
            {
                stateMachine.ProcessStateTransitions();
                Thread.Sleep(10);
            }


        }
    }
}
