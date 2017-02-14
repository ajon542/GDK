using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDK.StateMachine;

namespace GDK.GameLibrary
{
	/// <summary>
	/// StatePlay.
	/// </summary>
	public class StatePlay : BaseState
	{
		/// <summary>
		/// Configure the state in the given state machine.
		/// </summary>
		/// <param name="stateMachine">The state machine.</param>
		public override void Configure (GameStateMachine stateMachine)
		{
			stateMachine.StateMachine.Configure ("StatePlay")
                .OnEntry (OnEntry)
                .OnExit (OnExit)
                .Permit ("TriggerStatePayWin", "StatePayWin");
		}

		/// <summary>
		/// Entry method to the state.
		/// </summary>
		private void OnEntry ()
		{
			UnityEngine.Debug.Log ("OnEntry StatePlay");
		}

		/// <summary>
		/// Exit method from the state.
		/// </summary>
		private void OnExit ()
		{
			UnityEngine.Debug.Log ("OnExit StatePlay");
		}
	}
}
