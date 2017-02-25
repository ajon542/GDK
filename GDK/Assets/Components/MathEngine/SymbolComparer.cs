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
        private Dictionary<Symbol, List<Symbol>> symbolMap = new Dictionary<Symbol,List<Symbol>>();

        public void Add(Symbol s1, Symbol s2)
        {
            // Add s1 to the map.
            if (!symbolMap.ContainsKey(s1))
            {
                symbolMap.Add(s1, new List<Symbol>());
            }
            if (!symbolMap[s1].Contains(s2))
            {
                symbolMap[s1].Add(s2);
            }

            // Add s2 to the map.
            if (!symbolMap.ContainsKey(s2))
            {
                symbolMap.Add(s2, new List<Symbol>());
            }
            if (!symbolMap[s2].Contains(s1))
            {
                symbolMap[s2].Add(s1);
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
