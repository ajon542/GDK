﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace GDK.MathEngine.Evaluators
{
	public class ScatterEvaluator : IEvaluator
	{
		private ReelWindow reelWindow;

		public SlotResults Evaluate (Paytable paytable, IRng rng)
		{
			SlotResults results = new SlotResults ();

			// Get random numbers for each reel.
			List<int> randomNumbers = new List<int> ();
			for (int reel = 0; reel < paytable.ReelGroup.Reels.Count; ++reel)
			{
				ReelStrip reelStrip = paytable.ReelGroup.Reels [reel].ReelStrip;
				randomNumbers.Add (rng.GetRandomNumber (reelStrip.Symbols.Count));
			}

			reelWindow = new ReelWindow (paytable.ReelGroup, randomNumbers);
			 
			List<PayCombo> payCombos = paytable.ScatterComboGroup.Combos;
			for (int combo = 0; combo < payCombos.Count; ++combo)
			{
				PayCombo payCombo = payCombos [combo];
				int bestPayAmount = 0;
				PayCombo bestPayCombo = null;

				// TODO: This assumes the scatter combo contains one type of symbol only.
				// In almost all cases, this will be true so leaving it for now.
				bool match = false;
				Symbol symbol = payCombo.Symbols[0];
				if (reelWindow.SymbolCount.ContainsKey (symbol) &&
					reelWindow.SymbolCount[symbol] >= payCombo.Symbols.Count)
				{
					match = true;
				}
					
				if (match && (payCombo.PayAmount >= bestPayAmount))
				{
					bestPayCombo = payCombo;
					bestPayAmount = payCombo.PayAmount;
				}

				// TODO: Add the payline the win occurred on.
				if (bestPayCombo != null)
				{
					results.Results.Add (new SlotResult { PayCombo = bestPayCombo, Payline = null });
				}
			}

			return results;
		}
	}
}