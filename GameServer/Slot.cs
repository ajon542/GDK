using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;

namespace GameServer
{
    public class StateDrawRandomNumbers : BaseState
    {
        public StateDrawRandomNumbers(string name)
            : base(name)
        {
        }

        public override void Init(GameStateMachine stateMachine)
        {
        }

        public void OnEntry()
        {
            Console.WriteLine("OnEntry StateDrawRandomNumbers");
        }

        public void OnExit()
        {
            Console.WriteLine("OnExit StateDrawRandomNumbers");
        }
    }

    public class Slot : Game
    {
        private StateDrawRandomNumbers stateDrawRandomNumbers;

        public override void ConfigureStates(GameStateMachine stateMachine)
        {
            // Base states must be initialized first.
            base.ConfigureStates(stateMachine);

            stateDrawRandomNumbers = new StateDrawRandomNumbers("StateDrawRandomNumbers");
            stateMachine.StateMachine.Configure("StateDrawRandomNumbers")
                .SubstateOf("StatePlay")
                .OnEntry(stateDrawRandomNumbers.OnEntry)
                .OnExit(stateDrawRandomNumbers.OnExit)
                .Permit("TriggerStateGameOver", "StateGameOver");
        }
    }
}
