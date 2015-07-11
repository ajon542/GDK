using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;

namespace SlotLibrary
{
    public class StateEvaluate : BaseState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateEvaluate"/> class.
        /// </summary>
        /// <param name="name">The name of the state.</param>
        public StateEvaluate(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Configure the state in the given state machine.
        /// </summary>
        /// <param name="stateMachine">The state machine.</param>
        public override void Configure(GameStateMachine stateMachine)
        {
            stateMachine.StateMachine.Configure("StateEvaluate")
                .SubstateOf("StatePlay")
                .OnEntry(OnEntry)
                .OnExit(OnExit)
                .Permit("TriggerStateGameOver", "StateGameOver");

            // Since we are a substate, we must Permit a transition to ourselves.
            stateMachine.StateMachine.Configure("StatePlay")
                .Permit("TriggerStateEvaluate", "StateEvaluate");
        }

        /// <summary>
        /// Entry method to the state.
        /// </summary>
        private void OnEntry()
        {
            Console.WriteLine("OnEntry StateEvaluate");
        }

        /// <summary>
        /// Exit method from the state.
        /// </summary>
        private void OnExit()
        {
            Console.WriteLine("OnExit StateEvaluate");
        }
    }
}
