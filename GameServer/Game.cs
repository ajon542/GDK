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
        }

        public void OnEntry()
        {
            Console.WriteLine("OnEntry StateIdle");
        }

        public void OnExit()
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
        }

        public void OnEntry()
        {
            Console.WriteLine("OnEntry StatePlay");
        }

        public void OnExit()
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
        }

        public void OnEntry()
        {
            Console.WriteLine("OnEntry StateGameOver");
        }

        public void OnExit()
        {
            Console.WriteLine("OnExit StateGameOver");
        }
    }


    public class Game
    {
        private StateIdle stateIdle;
        private StatePlay statePlay;
        private StateGameOver stateGameOver;

        public virtual void ConfigureStates(GameStateMachine stateMachine)
        {
            stateIdle = new StateIdle("StateIdle");
            statePlay = new StatePlay("StatePlay");
            stateGameOver = new StateGameOver("StateGameOver");

            stateMachine.StateMachine.Configure("StateIdle")
                .OnEntry(stateIdle.OnEntry)
                .OnExit(stateIdle.OnExit)
                .Permit("TriggerStatePlay", "StatePlay");

            stateMachine.StateMachine.Configure("StatePlay")
                .OnEntry(statePlay.OnEntry)
                .OnExit(statePlay.OnExit)
                .Permit("TriggerStateDrawRandomNumbers", "StateDrawRandomNumbers") // TODO: This is an issue because we shouldn't have to know anything about substates
                .Permit("TriggerStateGameOver", "StateGameOver");

            stateMachine.StateMachine.Configure("StateGameOver")
                .OnEntry(stateGameOver.OnEntry)
                .OnExit(stateGameOver.OnExit)
                .Permit("TriggerStateIdle", "StateIdle");
        }
    }
}
