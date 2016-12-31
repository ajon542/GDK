using UnityEngine;
using System.Collections;
using StateMachine;

namespace Reels
{
	/// <summary>
	/// StateSpinning.
	/// </summary>
	public class StateSpinning : BaseState
	{
		/// <summary>
		/// Configure the state in the given state machine.
		/// </summary>
		/// <param name="stateMachine">The state machine.</param>
		public override void Configure (GameStateMachine stateMachine)
		{
			stateMachine.StateMachine.Configure ("StateSpinning")
				.OnEntry (OnEntry)
				.OnExit (OnExit)
				.Permit ("TriggerStateStopping", "StateStopping");
		}

		/// <summary>
		/// Entry method to the state.
		/// </summary>
		private void OnEntry ()
		{
			UnityEngine.Debug.Log ("OnEntry StateSpinning");
		}

		/// <summary>
		/// Exit method from the state.
		/// </summary>
		private void OnExit ()
		{
			UnityEngine.Debug.Log ("OnExit StateSpinning");
		}
	}
}