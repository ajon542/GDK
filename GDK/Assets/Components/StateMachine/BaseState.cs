using System;

namespace StateMachine
{
	/// <summary>
	/// Base class for all state objects.
	/// </summary>
	public abstract class BaseState
	{
		/// <summary>
		/// Configure the state in the given state machine.
		/// </summary>
		/// <param name="stateMachine">The state machine.</param>
		public abstract void Configure (GameStateMachine stateMachine);
	}
}