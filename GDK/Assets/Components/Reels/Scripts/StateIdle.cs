using UnityEngine;
using System.Collections;
using StateMachine;

namespace Reels
{
	/// <summary>
	/// StateIdle.
	/// </summary>
	public class StateIdle : BaseMonoBehaviourState
	{
		/// <summary>
		/// Configure the state in the given state machine.
		/// </summary>
		/// <param name="stateMachine">The state machine.</param>
		public override void Configure (GameStateMachine stateMachine)
		{
			stateMachine.StateMachine.Configure ("StateIdle")
				.OnEntry (OnEntry)
				.OnExit (OnExit)
				.Permit ("TriggerStateSpinning", "StateSpinning");
		}

		/// <summary>
		/// Entry method to the state.
		/// </summary>
		protected override void OnEntry ()
		{
			UnityEngine.Debug.Log ("OnEntry StateIdle");
			base.OnEntry ();
		}

		/// <summary>
		/// Exit method from the state.
		/// </summary>
		protected override void OnExit ()
		{
			UnityEngine.Debug.Log ("OnExit StateIdle");
			base.OnExit ();
		}

		private void Update()
		{
			if (IsActive == false)
			{
				return;
			}
		}
	}
}