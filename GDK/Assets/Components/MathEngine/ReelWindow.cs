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
	public class ReelWindow
	{
		// TODO: Might need the positions of the symbols.
		public List<List<Symbol>> Window { get; set; }

		public Dictionary<Symbol, int> SymbolCount { get; set; }

		public ReelWindow (ReelGroup reelGroup, List<int> randomNumbers)
		{
			UpdateReelWindow (reelGroup, randomNumbers);
		}

		public List<Symbol> GetSymbolsInPayline (Payline payline)
		{
			List<Symbol> symbolsInPayline = new List<Symbol> ();
			List<PaylineCoord> paylineCoords = payline.PaylineCoords;

			foreach (PaylineCoord paylineCoord in paylineCoords)
			{
				int reelIndex = paylineCoord.ReelIndex;
				int offset = paylineCoord.Offset;

				Symbol symbol = Window [reelIndex] [offset];

				symbolsInPayline.Add (symbol);
			}

			return symbolsInPayline;
		}
			
		public void UpdateReelWindow(ReelGroup reelGroup, List<int> randomNumbers)
		{
			Window = new List<List<Symbol>> ();
			SymbolCount = new Dictionary<Symbol, int> ();

			for (int reelIndex = 0; reelIndex < reelGroup.Reels.Count; ++reelIndex)
			{
				Window.Add (new List<Symbol> ());

				int reelHeight = reelGroup.Reels [reelIndex].Height;
				ReelStrip reelStrip = reelGroup.Reels [reelIndex].ReelStrip;

				int randomNumber = randomNumbers [reelIndex];
				for (int offset = 0; offset < reelHeight; ++offset)
				{
					int stripIndex = (randomNumber + offset) % reelStrip.Strip.Count;

					// Add the symbol to the reel window.
					Symbol symbol = new Symbol(reelStrip.Strip [stripIndex]);
					Window [reelIndex].Add (new Symbol (symbol));

					// Update the symbol count in the dictionary.
					if (SymbolCount.ContainsKey (symbol) == false)
						SymbolCount.Add (symbol, 0);
					SymbolCount [symbol]++;
				}
			}
		}
	}
}
