using UnityEngine;
using System.Collections;
using GDK.StateMachine;
using GDK.GameLibrary;
using GDK.SlotLibrary;

//Wallet
//-Credits

//HUD
//-Credits, Bet, Win
//-Bet button
//-Spin button
//-Info button

//BaseGame
//- At start of basegame, the HUD will disable input
//- At end of basegame, the HUD will enable input and display basegame wins

//FreeGames
//- At end of each freegame, the HUD will display the wins

//BonusGames
//- These are game specific and may update the values in the HUD in any way
//- Possibly creating their own HUD i.e. double up or a pickem game



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
