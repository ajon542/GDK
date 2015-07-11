﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;

namespace GameLibrary
{
    /// <summary>
    /// StateGameOver.
    /// </summary>
    public class StateGameOver : BaseState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateGameOver"/> class.
        /// </summary>
        /// <param name="name">The name of the state.</param>
        public StateGameOver(string name)
            : base(name)
        {
        }

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
            Console.WriteLine("OnEntry StateGameOver");
        }

        /// <summary>
        /// Exit method from the state.
        /// </summary>
        private void OnExit()
        {
            Console.WriteLine("OnExit StateGameOver");
        }
    }
}
