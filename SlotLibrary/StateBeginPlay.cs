using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;

namespace SlotLibrary
{
    public class StateBeginPlay : BaseState
    {
        /// <summary>
        /// Configure the state in the given state machine.
        /// </summary>
        /// <param name="stateMachine">The state machine.</param>
        public override void Configure(GameStateMachine stateMachine)
        {
            stateMachine.StateMachine.Configure("StateBeginPlay")
                .SubstateOf("StatePlay")
                .OnEntry(OnEntry)
                .OnExit(OnExit)
                .Permit("TriggerStateEvalute", "StateEvaluate");

            // Since we are a substate, we must Permit a transition to ourselves.
            stateMachine.StateMachine.Configure("StatePlay")
                .Permit("TriggerStateBeginPlay", "StateBeginPlay");
        }

        /// <summary>
        /// Entry method to the state.
        /// </summary>
        private void OnEntry()
        {
            Console.WriteLine("OnEntry StateBeginPlay");
        }

        /// <summary>
        /// Exit method from the state.
        /// </summary>
        private void OnExit()
        {
            Console.WriteLine("OnExit StateBeginPlay");
        }
    }
}
