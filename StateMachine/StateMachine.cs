using System.Collections.Generic;
using Stateless;
using Utilities;

namespace StateMachine
{
    /// <summary>
    /// Container for the game state machine.
    /// </summary>
    public class GameStateMachine
    {
        #region Initial State

        /// <summary>
        /// This is the initial state when the state machine is created.
        /// </summary>
        private class StateUnknown : BaseState
        {
            /// <summary>
            /// Configure the state in the given state machine.
            /// </summary>
            /// <param name="stateMachine">The state machine.</param>
            public override void Configure(GameStateMachine stateMachine)
            {
                // Configure this state to transition only to StateIdle.
                // Once we have transitioned to StateIdle, we will never
                // re-enter this state.
                stateMachine.StateMachine.Configure("StateUnknown")
                    .Permit("TriggerStateConfiguration", "StateConfiguration");
            }
        }

        #endregion

        /// <summary>
        /// Gets the handle to the state machine.
        /// </summary>
        public StateMachine<string, string> StateMachine { get; private set; }

        /// <summary>
        /// Internal queue for processing the state transitions.
        /// </summary>
        private Queue<string> queue = new Queue<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameStateMachine"/> class.
        /// </summary>
        public GameStateMachine()
        {
            BaseState stateUnknown = new StateUnknown();
            StateMachine = new StateMachine<string, string>("StateUnknown");
            stateUnknown.Configure(this);
        }

        /// <summary>
        /// Determine if there are any outstanding triggers. If so, transition to
        /// the next state.
        /// </summary>
        public void ProcessStateTransitions()
        {
            if (queue.Count > 0)
            {
                string trigger = queue.Dequeue();
                
                // Check if there are any valid transitions permitted for the trigger.
                if (StateMachine.CanFire(trigger))
                {
                    StateMachine.Fire(trigger);
                }
                else
                {
                    Log.Fail("No valid transitions are permitted from state '{0}' for trigger '{1}'", StateMachine.State, trigger);
                }
            }
        }

        /// <summary>
        /// Add a trigger to the state transition queue.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        public void AddTrigger(string trigger)
        {
            queue.Enqueue(trigger);
        }
    }
}
