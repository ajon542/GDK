using UnityEngine;
using System.Collections;
using StateMachine;

namespace Reels
{
	/// <summary>
	/// StateSpinning.
	/// </summary>
	public class StateSpinning : BaseMonoBehaviourState
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
		protected override void OnEntry ()
		{
			UnityEngine.Debug.Log ("OnEntry StateSpinning");
			base.OnEntry ();
		}

		/// <summary>
		/// Exit method from the state.
		/// </summary>
		protected override void OnExit ()
		{
			UnityEngine.Debug.Log ("OnExit StateSpinning");
			base.OnExit ();
		}

		private void Update()
		{
			if (IsActive == false)
			{
				return;
			}

			UnityEngine.Debug.Log ("StateSpinning...");
		}
	}
}