using UnityEngine;
using System.Collections.Generic;
using StateMachine;

namespace Reels
{
	public class ReelController : MonoBehaviour
	{
		private GameStateMachine stateMachine;

		[SerializeField]
		private List<BaseMonoBehaviourState> states;

		void Start ()
		{
			// Create the state machine.
			stateMachine = new GameStateMachine ();

			foreach (BaseMonoBehaviourState state in states)
			{
				state.Configure (stateMachine);
			}

			StartConfiguration ();
			StartIdle ();
		}

		void Update ()
		{
			stateMachine.ProcessStateTransitions ();

			if (Input.GetKeyDown (KeyCode.Space))
			{
				StartSpinning ();
				StartStopping ();
				StartIdle ();
			}
		}

		// TODO: This kinda sucks, if each state provides the triggers, how will we know about them here?
		public virtual void StartConfiguration()
		{
			stateMachine.AddTrigger ("TriggerStateConfiguration");
		}

		public virtual void StartIdle()
		{
			stateMachine.AddTrigger ("TriggerStateIdle");
		}

		public virtual void StartSpinning()
		{
			stateMachine.AddTrigger ("TriggerStateSpinning");
		}

		public virtual void StartStopping ()
		{
			stateMachine.AddTrigger ("TriggerStateStopping");
		}
	}
}
