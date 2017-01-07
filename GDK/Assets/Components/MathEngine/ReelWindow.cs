using System;
using System.Collections.Generic;

namespace GDK.MathEngine
{
	/// <summary>
	/// The reel window is defined by a set of random numbers. The random numbers provided
	/// are where the first index of the reel window starts. It might be easier to explain via a picture.
	/// </summary>
	/// <remarks>
	/// Imagine this is how our reel window is set up:
	/// 
	///     0  [ ][ ][ ][ ][ ]
	///     1  [ ]   [ ][ ][ ]
	///     2  [ ]   [ ]   [ ]
	///     3        [ ]   [ ]
	///     4              [ ]
	/// 
	/// If we are given the random numbers [1, 14, 23, 9, 17], the reel window would be mapped as follows:
	/// 
	///     0  [ 1][14][23][ 9][17]
	///     1  [ 2]    [24][10][18]
	///     2  [ 3]    [25]    [19]
	///     3          [26]    [20]
	///     4                  [21]
	/// 
	/// </remarks>
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

		public static List<List<Symbol>> GetReelWindow()
		{
			throw new NotImplementedException ();
		}

		public static bool ContainsAllSymbols(ReelGroup reelGroup, List<Symbol> symbol, List<int> randomNumbers)
		{
			throw new NotImplementedException ();
		}

		public static int GetSymbolCount(ReelGroup reelGroup, Symbol symbol, List<int> randomNumbers)
		{
			int symbolCount = 0;

			for (int reelIndex = 0; reelIndex < reelGroup.Reels.Count; ++reelIndex)
			{
				// Get the height of the individual reel window.
				int reelHeight = reelGroup.Reels [reelIndex].Height;

				// Count the given symbols in that reel window.
				ReelStrip reelStrip = reelGroup.Reels [reelIndex].ReelStrip;

				int randomNumber = randomNumbers [reelIndex];
				for (int offset = 0; offset < reelHeight; ++offset)
				{
					int stripIndex = (randomNumber + offset) % reelStrip.Strip.Count;

					if (reelStrip.Strip [stripIndex].Symbol == symbol)
					{
						++symbolCount;
					}
				}
			}

			return symbolCount;
		}
	}
}
