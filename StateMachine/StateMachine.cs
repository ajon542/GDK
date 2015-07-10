using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stateless;

namespace StateMachine
{
    public class StateMachine
    {
        StateMachine<State, Trigger> phoneCall;

        Queue<Trigger> queue = new Queue<Trigger>();

        private enum State
        {
            OffHook,
            Ringing,
            Connected,
            OnHold
        }

        private enum Trigger
        {
            CallDialled,
            HungUp,
            CallConnected,
            LeftMessage,
            PlacedOnHold
        }

        public void Init()
        {
            phoneCall = new StateMachine<State, Trigger>(State.OffHook);

            // State Offhook
            phoneCall.Configure(State.OffHook)
                .OnEntry(() => OnEntryOffHook())
                .OnExit(() => OnExitOffHook())
                .Permit(Trigger.CallDialled, State.Ringing);

            // State Ringing
            phoneCall.Configure(State.Ringing)
                .OnEntry(() => OnEntryRinging())
                .OnExit(() => OnExitRinging())
                .Permit(Trigger.HungUp, State.OffHook)
                .Permit(Trigger.CallConnected, State.Connected);

            // State Connected
            phoneCall.Configure(State.Connected)
                .OnEntry(() => OnEntryConnected())
                .OnExit(() => OnExitConnected())
                .Permit(Trigger.LeftMessage, State.OffHook)
                .Permit(Trigger.HungUp, State.OffHook)
                .Permit(Trigger.PlacedOnHold, State.OnHold);

            // State OnHold
            phoneCall.Configure(State.OnHold)
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
                phoneCall.Fire(trigger);
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
    }
}
