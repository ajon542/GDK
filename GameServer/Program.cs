using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;

// TODO: GameServer probably doesn't need reference to all these.
using MathEngine;
using StateMachine;
using GameLibrary;
using SlotLibrary;
using Utilities;

using Newtonsoft.Json;


namespace GameServer
{
    public class BaseMsg
    {
        public int Type { get; set; }
    }

    public class Account : BaseMsg
    {
        public string Email { get; set; }
        public bool Active { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //BaseMsg acc = new Account { Type = 102, Email = "blah@blah.com", Active = true };
            //string json = JsonConvert.SerializeObject(acc, Formatting.Indented);
            //Console.WriteLine(json);

            //BaseMsg result = JsonConvert.DeserializeObject<BaseMsg>(json);
            //if (result.Type == 102)
            //{
            //    BaseMsg accResult = JsonConvert.DeserializeObject<Account>(json);
            //}

            //Console.ReadLine();

            // Example of (de)serializing a paytable.
            //PaytableGenerator paytableGenerator = new PaytableGenerator();
            //byte[] data = paytableGenerator.Serialize();
            //paytableGenerator.Deserialize(data);

            //Paytable paytable = new Paytable();
            //paytable.ConstructDummyPaytable();

            //Evaluator evaluator = new Evaluator();
            //evaluator.Evaluate(paytable);

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

                // How do we know to trigger this substate?
                // Something must call "TriggerStateBeginPlay" in order to transition to the substate.
                // If nothing calls this trigger, then something must call "TriggerStatePayWin".
                //stateMachine.AddTrigger("TriggerStateBeginPlay");
                //stateMachine.AddTrigger("TriggerStateEvaluate");
            //stateMachine.AddTrigger("TriggerStatePayWin");

            // Consider the scenario where the game asks the platform if it can begin a game.
            // The platform responds OK and the method StartGameResponse() is called.
            // The idea behind this is if Slot overrides this method, it will trigger all the
            // appropriate Slot specific substates.
            // If the method isn't overridden, it will just trigger the Game states.
            // In this case, the Slot overrides this method and calls:
            //     stateMachine.AddTrigger("TriggerStateBeginPlay");
            //     stateMachine.AddTrigger("TriggerStateEvaluate");
            // It will then call the base class method which in turn calls:
            //     stateMachine.AddTrigger("TriggerStatePayWin");
            game.StartGameResponse();

            stateMachine.AddTrigger("TriggerStateGameOver");
            stateMachine.AddTrigger("TriggerStateIdle");

            ClientComms.Server server = new ClientComms.Server();
            server.Listen();

            // Process the pending triggers and transition to the states.
            while (true)
            {
                stateMachine.ProcessStateTransitions();
                Thread.Sleep(10);
            }
        }
    }
}
