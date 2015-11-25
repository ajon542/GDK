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
    /// State for reporting all wins to the platform.
    /// </summary>
    public class StatePayWin : BaseState
    {
        /// <summary>
        /// Configure the state in the given state machine.
        /// </summary>
        /// <param name="stateMachine">The state machine.</param>
        public override void Configure(GameStateMachine stateMachine)
        {
            stateMachine.StateMachine.Configure("StatePayWin")
                .OnEntry(OnEntry)
                .OnExit(OnExit)
                .Permit("TriggerStateGameOver", "StateGameOver");
        }

        /// <summary>
        /// Entry method to the state.
        /// </summary>
        private void OnEntry()
        {
            Log.Info("OnEntry StatePayWin");
        }

        /// <summary>
        /// Exit method from the state.
        /// </summary>
        private void OnExit()
        {
            Log.Info("OnExit StatePayWin");
        }
    }
}
