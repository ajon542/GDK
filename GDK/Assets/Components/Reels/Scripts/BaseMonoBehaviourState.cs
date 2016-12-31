using UnityEngine;
using System.Collections;

using StateMachine;

namespace Reels
{
	public class BaseMonoBehaviourState : MonoBehaviour
	{
		[SerializeField]
		private string stateName;

		[SerializeField]
		private string trigger;

		[SerializeField]
		private string destinationState;

		protected bool IsActive { get; private set; }

		public virtual void Configure (GameStateMachine stateMachine)
		{
			stateMachine.StateMachine.Configure (stateName)
				.OnEntry (OnEntry)
				.OnExit (OnExit)
				.Permit (trigger, destinationState);
		}

		protected virtual void OnEntry()
		{
			IsActive = true;
			Debug.Log ("OnEntry " + stateName);
		}

		protected virtual void OnExit()
		{
			IsActive = false;
			Debug.Log ("OnExit " + stateName);
		}
	}
}

