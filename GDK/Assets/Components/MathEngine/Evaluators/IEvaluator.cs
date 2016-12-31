using UnityEngine;
using System.Collections;

namespace GDK.MathEngine.Evaluator
{
	/// <summary>
	/// Interface for evaluating a paytable.
	/// </summary>
	/// <remarks>
	/// The general idea is when the paytable is evaluated, an entire game is played out.
	/// This includes base game, pick 'em games and free spins. The results of the game are
	/// stored in the SlotsResults structure and passed over to the presentation to play out.
	/// </remarks>
	public interface IEvaluator
	{
		SlotResults Evaluate (Paytable paytable, IRng rng);
	}
}
