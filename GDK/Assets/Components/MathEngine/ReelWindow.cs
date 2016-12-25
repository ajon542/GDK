using System;
using System.Collections.Generic;

namespace GDK.MathEngine
{
	public static class ReelWindow
	{
		public static List<Symbol> GetSymbolsInPayline (ReelGroup reelGroup, List<int> randomNumbers, Payline payline)
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
				ReelStrip reelStrip = reelGroup.Reels [reelIndex].ReelStrip;
				int stripIndex = (randomNumber + paylineCoord.Offset) % reelStrip.Strip.Count;

				// Add that symbol to the payline.
				symbolsInPayline.Add (reelStrip.Strip [stripIndex].Symbol);
			}

			return symbolsInPayline;
		}
	}
}
