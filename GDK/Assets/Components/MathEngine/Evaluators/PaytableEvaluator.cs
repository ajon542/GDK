using System;
using System.Collections.Generic;

namespace GDK.MathEngine.Evaluators
{
	/// <summary>
	/// Class to handle the evaluation of a paytable.
	/// </summary>
	/// <remarks>
	/// Evaluate base game
	/// Evaluate pick 'em features
	/// Evaluate free games
	/// 
	/// Well, base game and free games probably require some sort of "ReelEvaluator" but
	/// for now what we have is good enough.
	/// </remarks>
	public class PaytableEvaluator : IEvaluator
	{
		private IRng rng;

		public SlotResults Evaluate (Paytable paytable, IRng rng)
		{
			PaylineEvaluator paylineEvaluator = new PaylineEvaluator ();
			ScatterEvaluator scatterEvaluator = new ScatterEvaluator ();

			SlotResults paylineResults;
			SlotResults scatterResults;
			paylineResults = paylineEvaluator.Evaluate (paytable, rng);
			scatterResults = scatterEvaluator.Evaluate (paytable, rng);

			return results;
		}
	}
}
