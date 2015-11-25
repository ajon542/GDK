using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;
using Utilities;

namespace GameLibrary
{
    /// <summary>
    /// State to configure game with the appropriate paytables.
    /// </summary>
    public class StateConfiguration : BaseState
    {
        /// <summary>
        /// Configure the state in the given state machine.
        /// </summary>
        /// <param name="stateMachine">The state machine.</param>
        public override void Configure(GameStateMachine stateMachine)
        {
            stateMachine.StateMachine.Configure("StateConfiguration")
                .OnEntry(OnEntry)
                .OnExit(OnExit)
                .Permit("TriggerStateIdle", "StateIdle");
        }

        /// <summary>
        /// Entry method to the state.
        /// </summary>
        private void OnEntry()
        {
            Log.Info("OnEntry StateConfiguration");
        }

        /// <summary>
        /// Exit method from the state.
        /// </summary>
        private void OnExit()
        {
            Log.Info("OnExit StateConfiguration");
        }
    }
}
