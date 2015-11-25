using System;
using System.Collections.Generic;
using Utilities;

namespace MathEngine
{
    public class Evaluator
    {
        private List<Symbol> GetSymbolsInPayline(Paytable paytable, Payline payline)
        {
            List<Symbol> symbolsInPayline = new List<Symbol>();

            List<PaylineCoord> paylineCoords = payline.PaylineCoords;
            foreach (PaylineCoord paylineCoord in paylineCoords)
            {
                // TODO: Use a random number.
                int randomNumber = 0;
                int reelIndex = paylineCoord.ReelIndex;
                int stripIndex = randomNumber + paylineCoord.Offset;

                symbolsInPayline.Add(paytable.ReelGroup.Reels[reelIndex].Reel.Strip[stripIndex].Symbol);
            }

            return symbolsInPayline;
        }

        private bool CheckMatch(PayCombo payCombo, List<Symbol> symbolsInPayline)
        {
            if (payCombo.Symbols.Count > symbolsInPayline.Count)
            {
                return false;
            }

            bool match = true;
            for (int i = 0; i < payCombo.Symbols.Count; ++i)
            {
                if (!payCombo.Symbols[i].Equals(symbolsInPayline[i]))
                {
                    match = false;
                    break;
                }
            }

            return match;
        }

        public void Evaluate(Paytable paytable)
        {
            SlotResults results = new SlotResults();
            List<Symbol> symbolsInPayline = new List<Symbol>();

            // For each payline.
            List<Payline> paylines = paytable.PaylineGroup.Paylines;
            foreach (Payline payline in paylines)
            {
                // Get the symbols in the payline based on the reel window.
                symbolsInPayline = GetSymbolsInPayline(paytable, payline);

                // TODO: Create a pay evaluation strategy.
                // At this point we are looking for the best win on the given payline. This is a strategy.
                // For example: If the symbolsInPayline are "A A A", and the payCombo is "A A" with a pay of 10,
                // we record that pay combo as a win.
                // If we find another payCombo of say, "A A A" and that has a win of 100, this will override the
                // previously found payCombo for this line.
                // That being said, there are many other pay strategies that could be used.

                int bestPayAmount = 0;
                PayCombo bestPayCombo = null;

                List<PayCombo> payCombos = paytable.PayComboGroup.Combos;

                foreach (PayCombo payCombo in payCombos)
                {
                    bool match = CheckMatch(payCombo, symbolsInPayline);
                    if (match && (payCombo.PayAmount > bestPayAmount))
                    {
                        Log.Info("PayCombo Match: {0}", payCombo);
                        bestPayCombo = payCombo;
                        bestPayAmount = payCombo.PayAmount;
                    }
                }

                if (bestPayCombo != null)
                {
                    results.Results.Add(new SlotResult(bestPayCombo, payline));
                }

                symbolsInPayline.Clear();
            }
        }
    }
}
