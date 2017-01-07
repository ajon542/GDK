using UnityEngine;
using System.Collections.Generic;

namespace GDK.MathEngine.Evaluators
{
	public class ScatterEvaluator : IEvaluator
	{
		public SlotResults Evaluate (Paytable paytable, IRng rng)
		{
			SlotResults results = new SlotResults ();
			List<Symbol> symbolsInPayline = new List<Symbol> ();

			// Get random numbers for each reel.
			List<int> randomNumbers = new List<int> ();
			for (int reel = 0; reel < paytable.ReelGroup.Reels.Count; ++reel)
			{
				ReelStrip reelStrip = paytable.ReelGroup.Reels [reel].ReelStrip;
				randomNumbers.Add (rng.GetRandomNumber (reelStrip.Strip.Count));
			}

			// For each scatter combo, check whether 
			List<PayCombo> payCombos = paytable.ScatterComboGroup.Combos;
			for (int combo = 0; combo < payCombos.Count; ++combo)
			{
				//PayCombo scatterCombo = payCombos [combo];

				// TODO: Just take the first symbol in the scatter combo group.
				//bool symbolsInReelWindow = 
				//	ReelWindow.ContainsAllSymbols (paytable.ReelGroup, scatterCombo.Symbols, randomNumbers);

				// Look for the highest pay amount on a given payline.
				//int bestPayAmount = 0;
				//PayCombo bestPayCombo = null;

				//bool match = symbolCount >= scatterCombo.Count;
				//if (match && (scatterCombo.PayAmount >= bestPayAmount))
				//{
				//	bestPayCombo = scatterCombo;
				//	bestPayAmount = scatterCombo.PayAmount;
				//}

				//if (bestPayCombo != null)
				//{
				//	results.Results.Add (new SlotResult (bestPayCombo, payline));
				//}
			}

			return results;
		}
	}
}