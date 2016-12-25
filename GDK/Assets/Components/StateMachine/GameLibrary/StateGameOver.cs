using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateMachine;

namespace GameLibrary
{
    /// <summary>
    /// StateGameOver.
    /// </summary>
    public class StateGameOver : BaseState
    {
        /// <summary>
        /// Configure the state in the given state machine.
        /// </summary>
        /// <param name="stateMachine">The state machine.</param>
        public override void Configure(GameStateMachine stateMachine)
        {
            stateMachine.StateMachine.Configure("StateGameOver")
                .OnEntry(OnEntry)
                .OnExit(OnExit)
                .Permit("TriggerStateIdle", "StateIdle");
        }

        /// <summary>
        /// Entry method to the state.
        /// </summary>
        private void OnEntry()
        {
            UnityEngine.Debug.Log("OnEntry StateGameOver");
        }

        /// <summary>
        /// Exit method from the state.
        /// </summary>
        private void OnExit()
        {
            UnityEngine.Debug.Log("OnExit StateGameOver");
        }
    }
}
