using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEngine
{
    public class Evaluator
    {
        public void Evaluate(Paytable paytable)
        {
            List<Symbol> symbolsInPayline = new List<Symbol>();

            // For each payline.
            List<Payline> paylines = paytable.PaylineGroup.Paylines;
            foreach (Payline payline in paylines)
            {
                // For each payline coordinate.
                List<PaylineCoord> paylineCoords = payline.PaylineCoords;
                foreach (PaylineCoord paylineCoord in paylineCoords)
                {
                    int randomNumber = 0;
                    int reelIndex = paylineCoord.ReelIndex;
                    int stripIndex = randomNumber + paylineCoord.Offset;

                    symbolsInPayline.Add(paytable.ReelGroup.Reels[reelIndex].Reel.Strip[stripIndex].Symbol);

                    // Check if symbolsInPayline matches a pay combo.
                    List<PayCombo> payCombos = paytable.PayComboGroup.Combos;
                    foreach (PayCombo payCombo in payCombos)
                    {
                        // Compare payCombo.Symbols and symbolsInPayline
                        if (symbolsInPayline.Count >= payCombo.Symbols.Count)
                        {
                            // TODO:
                        }

                    }
                }

                symbolsInPayline.Clear();
                Console.WriteLine("--------------------");
            }
        }
    }
}
