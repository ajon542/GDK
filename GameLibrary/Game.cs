using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;

namespace GameLibrary
{
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
