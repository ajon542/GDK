using UnityEngine;
using System.Collections.Generic;
using RSG;

namespace GDK.Reels
{
	/// <summary>
	/// Interface for reel display.
	/// </summary>
	public interface IReelDisplay
	{
		/// <summary>
		/// Spin the reel, displaying all the symbols in the stream.
		/// </summary>
		/// <param name="symbolStream">
		/// The list of symbols to display while spinning. The reel will stop when
		/// there is no more symbols in the list.
		/// </param>
		Promise Spin (ISymbolStream symbolStream);

		/// <summary>
		/// Replace the symbol at the given index.
		/// </summary>
		/// <param name="index">The index of the symbol to replace.</param>
		/// <param name="symbol">The new symbol identifier.</param>
		void ReplaceSymbol (int index, string symbol);
	}
}
