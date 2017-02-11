using System.Collections.Generic;
using GDK.MathEngine;
using Zenject;

namespace GDK.Reels
{
    public class SymbolStream : ISymbolStream
    {
        //[Inject] private IRng rng;
        //[Inject] private Paytable paytable;

        private List<string> symbols = new List<string> { "AA", "BB", "CC", "DD", "EE", "FF", "GG" };
        private int current;

        public string NextSymbol()
        {
            if (current >= symbols.Count)
                current = 0;
            // TODO: Inject doesn't work if we create an instance of this class with "new".
            //List<ReelProperties> reelProps = paytable.ReelGroup.Reels;
            //List<Symbol> symbols = reelProps[0].ReelStrip.Symbols;

            // Add a random symbol from the reel for now...
            //int random = rng.GetRandomNumber(symbols.Count)

            return symbols[current++];
        }

        public string Peek()
        {
            return "AA";
        }
    }
}
