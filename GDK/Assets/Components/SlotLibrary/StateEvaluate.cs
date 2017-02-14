using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDK.StateMachine;

namespace GDK.SlotLibrary
{
	/// <summary>
	/// State for evaluation of the paytable to provide a game result.
	/// </summary>
	public class StateEvaluate : BaseState
	{
		/// <summary>
		/// Configure the state in the given state machine.
		/// </summary>
		/// <param name="stateMachine">The state machine.</param>
		public override void Configure (GameStateMachine stateMachine)
		{
			stateMachine.StateMachine.Configure ("StateEvaluate")
                .SubstateOf ("StatePlay")
                .OnEntry (OnEntry)
                .OnExit (OnExit)
                .Permit ("TriggerStatePayWin", "StatePayWin");

			// Since we are a substate, we must Permit a transition to ourselves.
			stateMachine.StateMachine.Configure ("StatePlay")
                .Permit ("TriggerStateEvaluate", "StateEvaluate");
		}

		/// <summary>
		/// Entry method to the state.
		/// </summary>
		private void OnEntry ()
		{
			UnityEngine.Debug.Log ("OnEntry StateEvaluate");
		}

		/// <summary>
		/// Exit method from the state.
		/// </summary>
		private void OnExit ()
		{
			UnityEngine.Debug.Log ("OnExit StateEvaluate");
		}
	}
}
