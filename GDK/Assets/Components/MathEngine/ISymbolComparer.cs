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
        /// Add matching symbols to the comparer.
        /// </summary>
        void Add(Symbol s1, Symbol s2);

        /// <summary>
        /// Query the comparer to determine if the symbols match.
        /// </summary>
        bool Match(Symbol s1, Symbol s2);
    }
}
