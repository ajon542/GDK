using UnityEngine;
using System.Collections;
using StateMachine;
using GameLibrary;
using SlotLibrary;

public class GameController : MonoBehaviour
{
	private GameStateMachine stateMachine;
	private Game game;

	void Start ()
	{
		// Create the state machine.
		stateMachine = new GameStateMachine ();

		// Create and configure the game.
		game = new Slot ();
		game.ConfigureStates (stateMachine);
		game.StartConfiguration ();
		game.StartIdle ();
	}

	void Update ()
	{
		stateMachine.ProcessStateTransitions ();

		if (Input.GetKeyDown (KeyCode.Space))
		{
			game.StartPlay ();
			game.StartPayWin ();
			game.StartGameOver ();
			game.StartIdle ();
		}
	}
}
