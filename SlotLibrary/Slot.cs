using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;
using GameLibrary;

namespace SlotLibrary
{
    public class StateDrawRandomNumbers : BaseState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateDrawRandomNumbers"/> class.
        /// </summary>
        /// <param name="name">The name of the state.</param>
        public StateDrawRandomNumbers(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Configure the state in the given state machine.
        /// </summary>
        /// <param name="stateMachine">The state machine.</param>
        public override void Configure(GameStateMachine stateMachine)
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

        /// <summary>
        /// Entry method to the state.
        /// </summary>
        private void OnEntry()
        {
            Console.WriteLine("OnEntry StateDrawRandomNumbers");
        }

        /// <summary>
        /// Exit method from the state.
        /// </summary>
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
            stateDrawRandomNumbers.Configure(stateMachine);
        }
    }
}
