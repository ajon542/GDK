using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEngine
{
    /// <summary>
    /// Represents a symbol on the reel strip.
    /// </summary>
    /// <remarks>
    /// A symbol on a reel strip can have additional properties such
    /// as weight.
    /// </remarks>
    public class ReelSymbol
    {
        /// <summary>
        /// Gets the symbol.
        /// </summary>
        public Symbol Symbol { get; set; }

        /// <summary>
        /// Gets the weight of the symbol.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReelSymbol"/> class.
        /// </summary>
        public ReelSymbol()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReelSymbol"/> class.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="weight">The weight of the symbol.</param>
        public ReelSymbol(Symbol symbol, int weight = 1)
        {
            Symbol = symbol;
            Weight = weight;
        }
    }

    /// <summary>
    /// Represents a reel strip.
    /// </summary>
    public class ReelStrip
    {
        /// <summary>
        /// Gets the reel strip.
        /// </summary>
        public List<ReelSymbol> Strip { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reel"/> class.
        /// </summary>
        public ReelStrip()
        {
            Strip = new List<ReelSymbol>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reel"/> class.
        /// </summary>
        /// <param name="strip">A strip of <see cref="ReelSymbol"/> objects.</param>
        public ReelStrip(List<ReelSymbol> strip)
        {
            Strip = new List<ReelSymbol>(strip);
        }

        /// <summary>
        /// Add a symbol to the reel strip.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="weight">The weight of the symbol.</param>
        public void AddSymbol(Symbol symbol, int weight = 1)
        {
            Strip.Add(new ReelSymbol(symbol, weight));
        }
    }
}
