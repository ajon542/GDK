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
            stateMachine.StateMachine.Configure("StateDrawRandomNumbers")
                .SubstateOf("StatePlay")
                .OnEntry(OnEntry)
                .OnExit(OnExit)
                .Permit("TriggerStateGameOver", "StateGameOver");

            // Since we are a substate, we must Permit a transition to ourselves.
            stateMachine.StateMachine.Configure("StatePlay")
                .Permit("TriggerStateDrawRandomNumbers", "StateDrawRandomNumbers");
        }

        private void OnEntry()
        {
            Console.WriteLine("OnEntry StateDrawRandomNumbers");
        }

        private void OnExit()
        {
            Console.WriteLine("OnExit StateDrawRandomNumbers");
        }
    }

    public class Slot : Game
    {
        private BaseState stateDrawRandomNumbers;

        public override void ConfigureStates(GameStateMachine stateMachine)
        {
            // Base states must be initialized first.
            base.ConfigureStates(stateMachine);

            stateDrawRandomNumbers = new StateDrawRandomNumbers("StateDrawRandomNumbers");
            stateDrawRandomNumbers.Init(stateMachine);
        }
    }
}
