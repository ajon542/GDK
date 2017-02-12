using System.Collections.Generic;
using GDK.MathEngine;
using GDK.Utilities;
using Zenject;

namespace GDK.Reels
{
    public class SymbolStream : ISymbolStream
    {
        private WInteger currentStop;
        private int spliceBuffer;
        private List<Symbol> symbols;
        private bool streamFinished;

        public void Initialize(List<Symbol> symbols)
        {
            this.symbols = symbols;
            currentStop = new WInteger(0, 0, symbols.Count);
        }

        public string NextSymbol()
        {
            string symbol = symbols[currentStop.Value].Name;
            currentStop.Subtract(1);
            spliceBuffer--;
            return symbol;
        }

        public string Peek()
        {
            if (streamFinished && spliceBuffer == 0)
                return string.Empty;
            else
                return symbols[currentStop.Value].Name;
        }

        public void Splice(int targetStop)
        {
            int totalSymbols = 7;
            int topBuffer = 3;

            streamFinished = true;
            currentStop = new WInteger(targetStop, 0, symbols.Count);
            currentStop.Add(totalSymbols);
            spliceBuffer = totalSymbols + topBuffer;
        }

        public void Reset()
        {
            streamFinished = false;
        }
    }
}
