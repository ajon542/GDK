using UnityEngine;
using System.Collections;
using StateMachine;
using GameLibrary;
using SlotLibrary;

public class GameController : MonoBehaviour
{
	GameStateMachine stateMachine;

	// Use this for initialization
	void Start ()
	{
		// Create the state machine.
		stateMachine = new GameStateMachine ();

		// Create and configure the game.
		Game game = new Slot ();
		game.ConfigureStates (stateMachine);

		// Example of adding state triggers.
		stateMachine.AddTrigger ("TriggerStateConfiguration");
		stateMachine.AddTrigger ("TriggerStateIdle");
		stateMachine.AddTrigger ("TriggerStatePlay");

		// How do we know to trigger this substate?
		// Something must call "TriggerStateBeginPlay" in order to transition to the substate.
		// If nothing calls this trigger, then something must call "TriggerStatePayWin".
		//stateMachine.AddTrigger("TriggerStateBeginPlay");
		//stateMachine.AddTrigger("TriggerStateEvaluate");
		//stateMachine.AddTrigger("TriggerStatePayWin");

		// Consider the scenario where the game asks the platform if it can begin a game.
		// The platform responds OK and the method StartGameResponse() is called.
		// The idea behind this is if Slot overrides this method, it will trigger all the
		// appropriate Slot specific substates.
		// If the method isn't overridden, it will just trigger the Game states.
		// In this case, the Slot overrides this method and calls:
		//     stateMachine.AddTrigger("TriggerStateBeginPlay");
		//     stateMachine.AddTrigger("TriggerStateEvaluate");
		// It will then call the base class method which in turn calls:
		//     stateMachine.AddTrigger("TriggerStatePayWin");
		game.StartGameResponse ();

		stateMachine.AddTrigger ("TriggerStateGameOver");
		stateMachine.AddTrigger ("TriggerStateIdle");
	}
	
	// Update is called once per frame
	void Update ()
	{
		stateMachine.ProcessStateTransitions ();
	}
}
