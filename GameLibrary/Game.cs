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

    /// <summary>
    /// StatePlay.
    /// </summary>
    public class StatePlay : BaseState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatePlay"/> class.
        /// </summary>
        /// <param name="name">The name of the state.</param>
        public StatePlay(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Configure the state in the given state machine.
        /// </summary>
        /// <param name="stateMachine">The state machine.</param>
        public override void Configure(GameStateMachine stateMachine)
        {
            stateMachine.StateMachine.Configure("StatePlay")
                .OnEntry(OnEntry)
                .OnExit(OnExit)
                .Permit("TriggerStateGameOver", "StateGameOver");
        }

        /// <summary>
        /// Entry method to the state.
        /// </summary>
        private void OnEntry()
        {
            Console.WriteLine("OnEntry StatePlay");
        }

        /// <summary>
        /// Exit method from the state.
        /// </summary>
        private void OnExit()
        {
            Console.WriteLine("OnExit StatePlay");
        }
    }

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

    public class Game
    {
        private BaseState stateIdle;
        private BaseState statePlay;
        private BaseState stateGameOver;

        public virtual void ConfigureStates(GameStateMachine stateMachine)
        {
            stateIdle = new StateIdle("StateIdle");
            statePlay = new StatePlay("StatePlay");
            stateGameOver = new StateGameOver("StateGameOver");
            stateIdle.Configure(stateMachine);
            statePlay.Configure(stateMachine);
            stateGameOver.Configure(stateMachine);
        }
    }
}
