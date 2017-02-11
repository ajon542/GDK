using System.Collections.Generic;
using GDK.MathEngine;
using Zenject;

namespace GDK.Reels
{
    public class SymbolStream : ISymbolStream
    {
        private int current;
        private List<Symbol> symbols;

        public void Initialize(List<Symbol> symbols)
        {
            this.symbols = symbols;
        }

        public string NextSymbol()
        {
            if (current >= symbols.Count)
                current = 0;
            return symbols[current++].Name;
        }

        public string Peek()
        {
            if (current >= symbols.Count)
                current = 0;
            return symbols[current].Name;
        }

        public void Splice(int targetStop)
        {
            // Here we have to account for the top buffer in the reels which is 
        }
    }
}
