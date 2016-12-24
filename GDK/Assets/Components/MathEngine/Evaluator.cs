using System;
using System.Collections.Generic;

namespace GDK.MathEngine
{
	public class Evaluator : IEvaluator
	{
		private IRng rng;

		private Paytable paytable;

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

			this.paytable = paytable;

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
				// Get the symbols in the payline based on the reel window.
				symbolsInPayline = GetSymbolsInPayline (randomNumbers, payline);

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
					bool match = CheckMatch (payCombo, symbolsInPayline);
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

		private List<Symbol> GetSymbolsInPayline (List<int> randomNumbers, Payline payline)
		{
			if (payline.PaylineCoords.Count > randomNumbers.Count)
			{
				throw new InvalidOperationException (string.Format ("cannot evaluate payline, not enough random numbers"));
			}

			List<Symbol> symbolsInPayline = new List<Symbol> ();
			List<PaylineCoord> paylineCoords = payline.PaylineCoords;

			int numberIndex = 0;
			foreach (PaylineCoord paylineCoord in paylineCoords)
			{
				// Get next random number for the reel.
				int randomNumber = randomNumbers [numberIndex++];
				int reelIndex = paylineCoord.ReelIndex;

				// Get the reel strip index based on the random number and the payline offset.
				ReelStrip reelStrip = paytable.ReelGroup.Reels [reelIndex].ReelStrip;
				int stripIndex = (randomNumber + paylineCoord.Offset) % reelStrip.Strip.Count;

				// Add that symbol to the payline.
				symbolsInPayline.Add (reelStrip.Strip [stripIndex].Symbol);
			}

			return symbolsInPayline;
		}

		private bool CheckMatch (PayCombo payCombo, List<Symbol> symbolsInPayline)
		{
			if (payCombo.Symbols.Count > symbolsInPayline.Count)
			{
				return false;
			}

			bool match = true;
			for (int i = 0; i < payCombo.Symbols.Count; ++i)
			{
				if (!payCombo.Symbols [i].Equals (symbolsInPayline [i]))
				{
					match = false;
					break;
				}
			}

			return match;
		}
	}
}
