using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;

namespace GameLibrary
{
    /// <summary>
    /// StateIdle.
    /// </summary>
    public class StateIdle : BaseState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateIdle"/> class.
        /// </summary>
        /// <param name="name">The name of the state.</param>
        public StateIdle(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Configure the state in the given state machine.
        /// </summary>
        /// <param name="stateMachine">The state machine.</param>
        public override void Configure(GameStateMachine stateMachine)
        {
            stateMachine.StateMachine.Configure("StateIdle")
                .OnEntry(OnEntry)
                .OnExit(OnExit)
                .Permit("TriggerStatePlay", "StatePlay");
        }

        /// <summary>
        /// Entry method to the state.
        /// </summary>
        private void OnEntry()
        {
            Console.WriteLine("OnEntry StateIdle");
        }

        /// <summary>
        /// Exit method from the state.
        /// </summary>
        private void OnExit()
        {
            Console.WriteLine("OnExit StateIdle");
        }
    }
}
