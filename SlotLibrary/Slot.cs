using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;
using GameLibrary;

namespace SlotLibrary
{
    public class Slot : Game
    {
        private BaseState stateBeginPlay;
        private BaseState stateEvaluate;

        public override void ConfigureStates(GameStateMachine stateMachine)
        {
            // Base states must be initialized first.
            base.ConfigureStates(stateMachine);

            stateBeginPlay = new StateBeginPlay("StateBeginPlay");
            stateBeginPlay.Configure(stateMachine);

            stateEvaluate = new StateEvaluate("StateEvaluate");
            stateEvaluate.Configure(stateMachine);
        }
    }
}
