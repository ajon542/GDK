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
        public virtual void ConfigureStates(GameStateMachine stateMachine)
        {
            StateIdle stateIdle = new StateIdle("StateIdle");
            StatePlay statePlay = new StatePlay("StatePlay");
            StateGameOver stateGameOver = new StateGameOver("StateGameOver");

            // StateIdle
            stateMachine.StateMachine.Configure(stateIdle.Name)
                .OnEntry(stateIdle.OnEntry)
                .OnExit(stateIdle.OnExit)
                .Permit("TriggerStatePlay", statePlay.Name);

            // StatePlay
            stateMachine.StateMachine.Configure(statePlay.Name)
                .OnEntry(statePlay.OnEntry)
                .OnExit(statePlay.OnExit)
                .Permit("TriggerStateGameOver", stateGameOver.Name);

            // StateGameOver
            stateMachine.StateMachine.Configure(stateGameOver.Name)
                .OnEntry(stateGameOver.OnEntry)
                .OnExit(stateGameOver.OnExit)
                .Permit("TriggerStateIdle", stateIdle.Name);

            stateMachine.StateMachine.Fire("TriggerStateIdle");
            stateMachine.StateMachine.Fire("TriggerStatePlay");
            stateMachine.StateMachine.Fire("TriggerStateGameOver");
            stateMachine.StateMachine.Fire("TriggerStateIdle");
        }
    }
}
