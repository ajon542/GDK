using UnityEngine;
using System.Collections;
using StateMachine;

namespace Reels
{
	public class ReelController : MonoBehaviour
	{
		private GameStateMachine stateMachine;
		private BaseState stateConfiguration;
		private BaseState stateIdle;
		private BaseState stateSpinning;
		private BaseState stateStopping;

		void Start ()
		{
			// Create the state machine.
			stateMachine = new GameStateMachine ();

			// Create and configure the reel states.
			stateConfiguration = new StateConfiguration ();
			stateIdle = new StateIdle ();
			stateSpinning = new StateSpinning ();
			stateStopping = new StateStopping ();

			stateConfiguration.Configure (stateMachine);
			stateIdle.Configure (stateMachine);
			stateSpinning.Configure (stateMachine);
			stateStopping.Configure (stateMachine);

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
