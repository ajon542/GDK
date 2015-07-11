using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stateless;

namespace StateMachine
{
    /// <summary>
    /// This is the initial state when the state machine is created.
    /// </summary>
    public class StateUnknown : BaseState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateUnknown"/> class.
        /// </summary>
        /// <param name="name">The name of the state.</param>
        public StateUnknown(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Configure the state in the given state machine.
        /// </summary>
        /// <param name="stateMachine">The state machine.</param>
        public override void Configure(GameStateMachine stateMachine)
        {
            // Configure this state to transition only to StateIdle.
            // Once we have transitioned to StateIdle, we will never
            // re-enter this state.
            stateMachine.StateMachine.Configure(Name)
                .Permit("TriggerStateConfiguration", "StateConfiguration");   
        }
    }

    /// <summary>
    /// Container for the game state machine.
    /// </summary>
    public class GameStateMachine
    {
        /// <summary>
        /// Gets the handle to the state machine.
        /// TODO: Probably shouldn't be accessible, wrap the basic SM functionality instead.
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
            BaseState stateUnknown = new StateUnknown("StateUnknown");
            StateMachine = new StateMachine<string, string>(stateUnknown.Name);
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
                StateMachine.Fire(trigger);
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
