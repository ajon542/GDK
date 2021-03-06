﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GDK.MathEngine.Evaluators
{
    public class ScatterEvaluator : IEvaluator
    {
        private ReelWindow reelWindow;

        public SlotResult Evaluate(Paytable paytable, ReelWindow reelWindow, IRng rng)
        {
            if (paytable.ScatterComboGroup == null)
            {
                throw new Exception("ScatterEvaluator cannot evalate a paytable without a ScatterComboGroup");
            }

            SlotResult result = new SlotResult();
            ScattersComponent component = new ScattersComponent();

            List<PayCombo> payCombos = paytable.ScatterComboGroup.Combos;
            for (int combo = 0; combo < payCombos.Count; ++combo)
            {
                PayCombo payCombo = payCombos[combo];
                int bestPayAmount = 0;
                PayCombo bestPayCombo = null;

                // TODO: This assumes the scatter combo contains one type of symbol only.
                // In almost all cases, this will be true so leaving it for now.
                bool match = false;
                Symbol symbol = payCombo.SymbolsInPayCombo[0];
                if (reelWindow.SymbolPositions.ContainsKey(symbol) &&
                    reelWindow.SymbolPositions[symbol].Count >= payCombo.SymbolsInPayCombo.Count)
                {
                    match = true;
                }

                if (match && (payCombo.PayAmount >= bestPayAmount))
                {
                    bestPayCombo = payCombo;
                    bestPayAmount = payCombo.PayAmount;
                }

                if (bestPayCombo != null)
                {
                    // TODO: Might be able to optimize this but making PaylineCoord and SymbolPosition the same thing...
                    Payline payline = new Payline();
                    foreach (var position in reelWindow.SymbolPositions[symbol])
                    {
                        payline.AddPaylineCoord(new PaylineCoord { ReelIndex = position.ReelIndex, Offset = position.ReelOffset });
                    }
                    component.PayResults.Add(new PayResult { PayCombo = bestPayCombo, Payline = payline });
                    result.TotalWin += bestPayCombo.PayAmount;
                }
            }

            if (component.PayResults.Count > 0)
            {
                result.AddComponent<ScattersComponent>(component);
            }

            return result;
        }
    }
}