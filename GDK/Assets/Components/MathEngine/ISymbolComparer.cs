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
        /// Imagine we have a Wild symbol that substitutes for any other symbol. This means the
        /// if we have a Wild symbol in the payline, it can be "replaced" with an Ace if needs be.
        /// </remarks>
        /// <param name="substitute">The symbol that can substitute.</param>
        /// <param name="symbol">The symbol that can be substituted for.</param>
        void Substitute(Symbol substitute, Symbol symbol);

        /// <summary>
        /// Query the comparer to determine if the symbols match.
        /// </summary>
        bool Match(Symbol s1, Symbol s2);
    }
}
