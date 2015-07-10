using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stateless;

namespace StateMachine
{
    public class StateIdle : BaseState
    {
        public StateIdle(string name)
            : base(name)
        {
        }

        public override void Init(StateMachine stateMachine)
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

        public override void Init(StateMachine stateMachine)
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

        public override void Init(StateMachine stateMachine)
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

    public class TriggerStateIdle : BaseTrigger
    {
        public TriggerStateIdle(string name)
            : base(name)
        {
        }
    }

    public class TriggerStatePlay : BaseTrigger
    {
        public TriggerStatePlay(string name)
            : base(name)
        {
        }
    }

    public class TriggerStateGameOver : BaseTrigger
    {
        public TriggerStateGameOver(string name)
            : base(name)
        {
        }
    }


    public class StateMachine
    {
        public StateMachine<BaseState, BaseTrigger> SM { get; set; }
        private Queue<BaseTrigger> queue = new Queue<BaseTrigger>();

        public void Init()
        {
            // States.
            StateIdle stateIdle = new StateIdle("StateIdle");
            StatePlay statePlay = new StatePlay("StatePlay");
            StateGameOver stateGameOver = new StateGameOver("StateGameOver");

            // Triggers.
            BaseTrigger triggerStateIdle = new TriggerStateIdle("TriggerStateIdle");
            BaseTrigger triggerStatePlay = new TriggerStatePlay("TriggerStatePlay");
            BaseTrigger triggerStateGameOver = new TriggerStateGameOver("TriggerStateGameOver");

            SM = new StateMachine<BaseState, BaseTrigger>(stateIdle);

            // StateIdle,
            SM.Configure(stateIdle)
                .OnEntry(() => stateIdle.OnEntry())
                .OnExit(() => stateIdle.OnExit())
                .Permit(triggerStatePlay, statePlay);

            // StatePlay
            SM.Configure(statePlay)
                .OnEntry(() => statePlay.OnEntry())
                .OnExit(() => statePlay.OnExit())
                .Permit(triggerStateGameOver, stateGameOver);

            // StateIdle
            SM.Configure(stateGameOver)
                .OnEntry(() => stateGameOver.OnEntry())
                .OnExit(() => stateGameOver.OnExit())
                .Permit(triggerStateIdle, stateIdle);

            SM.Fire(triggerStatePlay);
            SM.Fire(triggerStateGameOver);
            SM.Fire(triggerStateIdle);
        }
    }

    /*public class StateMachine
    {
        public StateMachine<State, Trigger> SM { get; set; }

        Queue<Trigger> queue = new Queue<Trigger>();

        public enum State
        {
            OffHook,
            Ringing,
            Connected,
            OnHold
        }

        public enum Trigger
        {
            CallDialled,
            HungUp,
            CallConnected,
            LeftMessage,
            PlacedOnHold
        }

        public void Init()
        {
            SM = new StateMachine<State, Trigger>(State.OffHook);

            // State Offhook
            SM.Configure(State.OffHook)
                .OnEntry(() => OnEntryOffHook())
                .OnExit(() => OnExitOffHook())
                .Permit(Trigger.CallDialled, State.Ringing);

            SM.Configure(State.OffHook)
                .OnEntry(() => OnEntryOffHook())
                .OnExit(() => OnExitOffHook())
                .Permit(Trigger.CallDialled, State.Ringing);

            // State Ringing
            SM.Configure(State.Ringing)
                .OnEntry(() => OnEntryRinging())
                .OnExit(() => OnExitRinging())
                .Permit(Trigger.HungUp, State.OffHook)
                .Permit(Trigger.CallConnected, State.Connected);

            // State Connected
            SM.Configure(State.Connected)
                .OnEntry(() => OnEntryConnected())
                .OnExit(() => OnExitConnected())
                .Permit(Trigger.LeftMessage, State.OffHook)
                .Permit(Trigger.HungUp, State.OffHook)
                .Permit(Trigger.PlacedOnHold, State.OnHold);

            // State OnHold
            SM.Configure(State.OnHold)
                .OnEntry(() => OnEntryOnHold())
                .OnExit(() => OnExitOnHold())
                .SubstateOf(State.Connected)
                .Permit(Trigger.HungUp, State.OffHook);
        }

        public void Run()
        {
            queue.Enqueue(Trigger.CallDialled);

            while (queue.Count > 0)
            {
                Trigger trigger = queue.Dequeue();
                SM.Fire(trigger);
            }
        }

        #region OffHook
        private void OnEntryOffHook()
        {
            Console.WriteLine("OnEntryOffHook");
        }

        private void OnExitOffHook()
        {
            Console.WriteLine("OnExitOffHook");
        }
        #endregion
        #region Ringing
        private void OnEntryRinging()
        {
            Console.WriteLine("OnEntryRinging");
            queue.Enqueue(Trigger.CallConnected);
        }

        private void OnExitRinging()
        {
            Console.WriteLine("OnExitRinging");
        }
        #endregion
        #region Connected
        private void OnEntryConnected()
        {
            Console.WriteLine("OnEntryConnected");
            queue.Enqueue(Trigger.PlacedOnHold);
        }

        private void OnExitConnected()
        {
            Console.WriteLine("OnExitConnected");
        }
        #endregion
        #region OnHold
        private void OnEntryOnHold()
        {
            Console.WriteLine("OnEntryOnHold");
            queue.Enqueue(Trigger.HungUp);
        }

        private void OnExitOnHold()
        {
            Console.WriteLine("OnExitOnHold");
        }
        #endregion
    }*/
}
