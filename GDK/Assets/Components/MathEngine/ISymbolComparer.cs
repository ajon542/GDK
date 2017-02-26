using UnityEngine;
using System.Collections;

namespace GDK.MathEngine
{
    /// <summary>
    /// Interface for symbol comparison.
    /// </summary>
    public interface ISymbolComparer
    {
        /// <summary>
        /// Specify a symbol which can substitute for another symbol.
        /// </summary>
        /// <remarks>
        /// A standard slot machine game will have a Wild symbol which can substitute
        /// for any other symbol (except for maybe the bonus symbol).
        /// </remarks>
        /// <param name="substitute">The symbol that can substitute.</param>
        /// <param name="symbol">The symbol that can be substituted for.</param>
        void Substitute(Symbol substitute, Symbol symbol);

        /// <summary>
        /// Query the comparer to determine if the symbols match.
        /// </summary>
        /// <remarks>
        /// This ties in closely with the Substitute method. If we specify that a
        /// Wild substitutes for an Ace, a Wild will not match an Ace, but an Ace will
        /// match a Wild.
        /// </remarks>
        bool Match(Symbol s1, Symbol s2);
    }
}
