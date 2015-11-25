using System;
using System.Collections.Generic;

namespace MathEngine
{
    /// <summary>
    /// Container for <see cref="PayCombo"/> and <see cref="Payline"/> which
    /// represents a single result for a slot style game.
    /// </summary>
    class SlotResult
    {
        public SlotResult(PayCombo payCombo, Payline payline)
        {
            // TODO: Probably should make a GDKException class.
            if (payCombo == null)
            {
                throw new ArgumentNullException("payCombo");
            }

            if (payline == null)
            {
                throw new ArgumentNullException("payline");
            }

            PayCombo = payCombo;
            Payline = payline;
        }

        public PayCombo PayCombo { get; private set; }
        public Payline Payline { get; private set; }
    }

    class SlotResults
    {
        public SlotResults()
        {
            Results = new List<SlotResult>();
        }

        public List<SlotResult> Results { get; set; }
    }
}
