using UnityEngine;
using System.Collections;
using StateMachine;

namespace Reels
{
	/// <summary>
	/// StateStopping.
	/// </summary>
	public class StateStopping : BaseMonoBehaviourState
	{
		/// <summary>
		/// Configure the state in the given state machine.
		/// </summary>
		/// <param name="stateMachine">The state machine.</param>
		public override void Configure (GameStateMachine stateMachine)
		{
			stateMachine.StateMachine.Configure ("StateStopping")
				.OnEntry (OnEntry)
				.OnExit (OnExit)
				.Permit ("TriggerStateIdle", "StateIdle");
		}

		/// <summary>
		/// Entry method to the state.
		/// </summary>
		protected override void OnEntry ()
		{
			UnityEngine.Debug.Log ("OnEntry StateStopping");
			base.OnEntry ();
		}

		/// <summary>
		/// Exit method from the state.
		/// </summary>
		protected override void OnExit ()
		{
			UnityEngine.Debug.Log ("OnExit StateStopping");
			base.OnExit ();
		}

		private void Update()
		{
			if (IsActive == false)
			{
				return;
			}

			UnityEngine.Debug.Log ("StateStopping...");
		}
	}
}