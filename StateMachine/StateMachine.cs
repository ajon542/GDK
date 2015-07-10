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
        public StateUnknown(string name)
            : base(name)
        {
        }

        public override void Init(GameStateMachine stateMachine)
        {
            // Configure this state to transition only to StateIdle.
            // Once we have transitioned to StateIdle, we will never
            // re-enter this state.
            stateMachine.StateMachine.Configure(Name)
                .Permit("TriggerStateIdle", "StateIdle");   
        }
    }

    public class GameStateMachine
    {
        public StateMachine<string, string> StateMachine { get; private set; }
        private Queue<string> queue = new Queue<string>();

        public GameStateMachine()
        {
            BaseState stateUnknown = new StateUnknown("StateUnknown");
            StateMachine = new StateMachine<string, string>(stateUnknown.Name);
            stateUnknown.Init(this);
        }

        public void ProcessStateTransitions()
        {
            if (queue.Count > 0)
            {
                string trigger = queue.Dequeue();
                StateMachine.Fire(trigger);
            }
        }

        public void AddTrigger(string trigger)
        {
            queue.Enqueue(trigger);
        }
    }
}
