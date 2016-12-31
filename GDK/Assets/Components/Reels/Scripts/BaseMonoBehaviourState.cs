using UnityEngine;
using System.Collections;

using StateMachine;

namespace Reels
{
	public abstract class BaseMonoBehaviourState : MonoBehaviour
	{
		protected bool IsActive { get; private set; }

		/// <summary>
		/// Configure the state in the given state machine.
		/// </summary>
		/// <param name="stateMachine">The state machine.</param>
		public abstract void Configure (GameStateMachine stateMachine);

		protected virtual void OnEntry()
		{
			IsActive = true;
		}

		protected virtual void OnExit()
		{
			IsActive = false;
		}
	}
}

