using UnityEngine;
using System.Collections.Generic;

namespace GDK.MathEngine
{
    /// <summary>
    /// This is the standard symbol comparer. Same-symbols will always match. All other
    /// matching symbols must be added. For example, if we want to specify that A matches W
    /// we have to add them to the comparer.
    /// </summary>
    public class SymbolComparer : ISymbolComparer
    {
        /// <summary>
        /// This maps 
        /// </summary>
        private Dictionary<Symbol, List<Symbol>> symbolMap = new Dictionary<Symbol,List<Symbol>>();

        public void Substitute(Symbol substitute, Symbol symbol)
        {
            if (!symbolMap.ContainsKey(symbol))
            {
                symbolMap.Add(symbol, new List<Symbol>());
            }
            if (!symbolMap[symbol].Contains(substitute))
            {
                symbolMap[symbol].Add(substitute);
            }
        }

        public bool Match(Symbol s1, Symbol s2)
        {
            // This comparer specifies the same symbols always match.
            if (s1.Equals(s2))
            {
                return true;
            }

            if (symbolMap.ContainsKey(s1))
            {
                return symbolMap[s1].Contains(s2);
            }

            return false;
        }
    }
}
