using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;
using GameLibrary;

namespace SlotLibrary
{
    /// <summary>
    /// Base class for slot games.
    /// </summary>
    public class Slot : Game
    {
        private BaseState stateBeginPlay;
        private BaseState stateEvaluate;

        /// <summary>
        /// Configure all the slot states.
        /// </summary>
        /// <param name="stateMachine">The game state machine.</param>
        public override void ConfigureStates(GameStateMachine stateMachine)
        {
            // Base states must be initialized first.
            base.ConfigureStates(stateMachine);

            // Substates of StatePlay.
            stateBeginPlay = new StateBeginPlay();
            stateBeginPlay.Configure(stateMachine);
                

            stateEvaluate = new StateEvaluate();
            stateEvaluate.Configure(stateMachine);
        }
    }
}
