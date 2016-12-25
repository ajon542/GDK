using System;
using System.Collections.Generic;

namespace GDK.MathEngine
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
	public class Evaluator : IEvaluator
	{
		private IRng rng;

		public SlotResults Evaluate (Paytable paytable, IRng rng)
		{
			if (paytable == null)
			{
				throw new ArgumentNullException ("paytable cannot be null");
			}

			if (rng == null)
			{
				throw new ArgumentNullException ("random number generator cannot be null");
			}

			SlotResults results = new SlotResults ();
			List<Symbol> symbolsInPayline = new List<Symbol> ();

			// Get random numbers for each reel.
			List<int> randomNumbers = new List<int> ();
			for (int reel = 0; reel < paytable.ReelGroup.Reels.Count; ++reel)
			{
				ReelStrip reelStrip = paytable.ReelGroup.Reels [reel].ReelStrip;
				randomNumbers.Add (rng.GetRandomNumber (reelStrip.Strip.Count));
			}

			List<Payline> paylines = paytable.PaylineGroup.Paylines;
			foreach (Payline payline in paylines)
			{
				// A payline is essentially just a list of offsets from the 0 position.
				// We add these offsets to the random number generated to obtain the final
				// index into the reel strip.
				symbolsInPayline = ReelWindow.GetSymbolsInPayline (paytable.ReelGroup, randomNumbers, payline);

				// At this point we are looking for the best win on the given payline.
				// For example: If the symbolsInPayline are "A A A", and the payCombo is "A A" with a pay of 10,
				// we record that pay combo as a win.
				// If we find another payCombo of say, "A A A" and that has a win of 100, this will override the
				// previously found payCombo for this line.
				// That being said, there are many other pay strategies that could be used.

				int bestPayAmount = 0;
				PayCombo bestPayCombo = null;

				List<PayCombo> payCombos = paytable.PayComboGroup.Combos;

				foreach (PayCombo payCombo in payCombos)
				{
					bool match = payCombo.IsMatch (symbolsInPayline);
					if (match && (payCombo.PayAmount > bestPayAmount))
					{
						bestPayCombo = payCombo;
						bestPayAmount = payCombo.PayAmount;
					}
				}

				if (bestPayCombo != null)
				{
					results.Results.Add (new SlotResult (bestPayCombo, payline));
				}

				symbolsInPayline.Clear ();
			}

			return results;
		}
	}
}
