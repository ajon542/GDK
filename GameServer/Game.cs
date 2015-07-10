using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;

namespace GameServer
{
    public class StateIdle : BaseState
    {
        public StateIdle(string name)
            : base(name)
        {
        }

        public override void Init(GameStateMachine stateMachine)
        {
            stateMachine.StateMachine.Configure("StateIdle")
                .OnEntry(OnEntry)
                .OnExit(OnExit)
                .Permit("TriggerStatePlay", "StatePlay");
        }

        private void OnEntry()
        {
            Console.WriteLine("OnEntry StateIdle");
        }

        private void OnExit()
        {
            Console.WriteLine("OnExit StateIdle");
        }
    }

    public class StatePlay : BaseState
    {
        public StatePlay(string name)
            : base(name)
        {
        }

        public override void Init(GameStateMachine stateMachine)
        {
            stateMachine.StateMachine.Configure("StatePlay")
                .OnEntry(OnEntry)
                .OnExit(OnExit)
                .Permit("TriggerStateGameOver", "StateGameOver");
        }

        private void OnEntry()
        {
            Console.WriteLine("OnEntry StatePlay");
        }

        private void OnExit()
        {
            Console.WriteLine("OnExit StatePlay");
        }
    }

    public class StateGameOver : BaseState
    {
        public StateGameOver(string name)
            : base(name)
        {
        }

        public override void Init(GameStateMachine stateMachine)
        {
            stateMachine.StateMachine.Configure("StateGameOver")
                .OnEntry(OnEntry)
                .OnExit(OnExit)
                .Permit("TriggerStateIdle", "StateIdle");
        }

        private void OnEntry()
        {
            Console.WriteLine("OnEntry StateGameOver");
        }

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
            stateIdle.Init(stateMachine);
            statePlay.Init(stateMachine);
            stateGameOver.Init(stateMachine);
        }
    }
}
