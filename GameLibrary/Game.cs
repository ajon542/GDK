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
        private BaseState stateConfiguration;
        private BaseState stateIdle;
        private BaseState statePlay;
        private BaseState statePayWin;
        private BaseState stateGameOver;

        public virtual void ConfigureStates(GameStateMachine stateMachine)
        {
            stateConfiguration = new StateConfiguration("StateConfgiuration");
            stateIdle = new StateIdle("StateIdle");
            statePlay = new StatePlay("StatePlay");
            statePayWin = new StatePayWin("StatePayWin");
            stateGameOver = new StateGameOver("StateGameOver");

            stateConfiguration.Configure(stateMachine);
            stateIdle.Configure(stateMachine);
            statePlay.Configure(stateMachine);
            statePayWin.Configure(stateMachine);
            stateGameOver.Configure(stateMachine);
        }
    }
}
