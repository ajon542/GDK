using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDK.MathEngine
{
	/// <summary>
	/// Represents a reel strip.
	/// </summary>
	[Serializable]
	public class ReelStrip
	{
		/// <summary>
		/// Gets the reel strip.
		/// </summary>
		public List<Symbol> Symbols { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Reel"/> class.
		/// </summary>
		public ReelStrip ()
		{
			Symbols = new List<Symbol> ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Reel"/> class.
		/// </summary>
		/// <param name="strip">A strip of <see cref="ReelSymbol"/> objects.</param>
		public ReelStrip (List<Symbol> strip)
		{
			Symbols = new List<Symbol> (strip);
		}

		/// <summary>
		/// Add a symbol to the reel strip.
		/// </summary>
		/// <param name="symbol">The symbol.</param>
		public void AddSymbol (Symbol symbol)
		{
			Symbols.Add (symbol);
		}
	}
}
