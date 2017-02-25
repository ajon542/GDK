using UnityEngine;
using System;
using System.Collections.Generic;

namespace GDK.MathEngine
{
    [Serializable]
    public class SymbolGroup
    {
        public List<Symbol> Symbols;

        public SymbolGroup()
        {
            Symbols = new List<Symbol>();
        }
    }
}