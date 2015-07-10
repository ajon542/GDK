using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stateless;

namespace StateMachine
{
    public class StateUnknown : BaseState
    {
        public StateUnknown(string name)
            : base(name)
        {
        }

        public override void Init(GameStateMachine stateMachine)
        {
        }
    }

    public class GameStateMachine
    {
        public StateMachine<string, string> StateMachine { get; private set; }
        private Queue<string> queue = new Queue<string>();

        public GameStateMachine()
        {
            StateUnknown stateUnknown = new StateUnknown("StateUnknown");
            StateMachine = new StateMachine<string, string>(stateUnknown.Name);
            StateMachine.Configure(stateUnknown.Name)
                .Permit("TriggerStateIdle", "StateIdle");   
        }
    }
}
