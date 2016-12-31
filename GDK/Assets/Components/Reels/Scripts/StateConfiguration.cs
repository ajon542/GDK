using UnityEngine;
using System.Collections;
using StateMachine;

namespace Reels
{
	/// <summary>
	/// State to configure game with the appropriate paytables.
	/// </summary>
	public class StateConfiguration : BaseMonoBehaviourState
	{
		/// <summary>
		/// Configure the state in the given state machine.
		/// </summary>
		/// <param name="stateMachine">The state machine.</param>
		public override void Configure (GameStateMachine stateMachine)
		{
			stateMachine.StateMachine.Configure ("StateConfiguration")
				.OnEntry (OnEntry)
				.OnExit (OnExit)
				.Permit ("TriggerStateIdle", "StateIdle");
		}

		/// <summary>
		/// Entry method to the state.
		/// </summary>
		protected override void OnEntry ()
		{
			UnityEngine.Debug.Log ("OnEntry StateConfiguration");
			base.OnEntry ();
		}

		/// <summary>
		/// Exit method from the state.
		/// </summary>
		protected override void OnExit ()
		{
			UnityEngine.Debug.Log ("OnExit StateConfiguration");
			base.OnExit ();
		}

		private void Update()
		{
			if (IsActive == false)
			{
				return;
			}

			UnityEngine.Debug.Log ("StateConfiguration...");
		}
	}
}
