using UnityEngine;
using System.Collections;

using GDK.MathEngine;

namespace GDK.GameSimulation
{
    public class SimulationPaytableBuilder : PaytableBuilder
    {
        public override ReelGroup BuildReelGroup()
        {
            ReelGroup reels = new ReelGroup();

            ReelStrip reel1 = new ReelStrip();
            reel1.AddSymbol(new Symbol(0, "AA"));
            reel1.AddSymbol(new Symbol(1, "BB"));
            reel1.AddSymbol(new Symbol(2, "CC"));

            ReelStrip reel2 = new ReelStrip();
            reel2.AddSymbol(new Symbol(0, "AA"));
            reel2.AddSymbol(new Symbol(1, "BB"));
            reel2.AddSymbol(new Symbol(2, "CC"));

            ReelStrip reel3 = new ReelStrip();
            reel3.AddSymbol(new Symbol(0, "AA"));
            reel3.AddSymbol(new Symbol(1, "BB"));
            reel3.AddSymbol(new Symbol(2, "CC"));

            ReelStrip reel4 = new ReelStrip();
            reel4.AddSymbol(new Symbol(0, "AA"));
            reel4.AddSymbol(new Symbol(1, "BB"));
            reel4.AddSymbol(new Symbol(2, "CC"));

            ReelStrip reel5 = new ReelStrip();
            reel5.AddSymbol(new Symbol(0, "AA"));
            reel5.AddSymbol(new Symbol(1, "BB"));
            reel5.AddSymbol(new Symbol(2, "CC"));

            reels.AddReel(reel1);
            reels.AddReel(reel2);
            reels.AddReel(reel3);
            reels.AddReel(reel4);
            reels.AddReel(reel5);

            return reels;
        }

        public override PaylineGroup BuildPaylineGroup()
        {
            // Paylines
            PaylineGroup paylines = new PaylineGroup();

            Payline payline1 = new Payline();
            payline1.AddPaylineCoord(new PaylineCoord { ReelIndex = 0, Offset = 0 });
            payline1.AddPaylineCoord(new PaylineCoord { ReelIndex = 1, Offset = 0 });
            payline1.AddPaylineCoord(new PaylineCoord { ReelIndex = 2, Offset = 0 });
            payline1.AddPaylineCoord(new PaylineCoord { ReelIndex = 3, Offset = 0 });
            payline1.AddPaylineCoord(new PaylineCoord { ReelIndex = 4, Offset = 0 });

            Payline payline2 = new Payline();
            payline2.AddPaylineCoord(new PaylineCoord { ReelIndex = 0, Offset = 1 });
            payline2.AddPaylineCoord(new PaylineCoord { ReelIndex = 1, Offset = 1 });
            payline2.AddPaylineCoord(new PaylineCoord { ReelIndex = 2, Offset = 1 });
            payline2.AddPaylineCoord(new PaylineCoord { ReelIndex = 3, Offset = 1 });
            payline2.AddPaylineCoord(new PaylineCoord { ReelIndex = 4, Offset = 1 });

            Payline payline3 = new Payline();
            payline3.AddPaylineCoord(new PaylineCoord { ReelIndex = 0, Offset = 2 });
            payline3.AddPaylineCoord(new PaylineCoord { ReelIndex = 1, Offset = 2 });
            payline3.AddPaylineCoord(new PaylineCoord { ReelIndex = 2, Offset = 2 });
            payline3.AddPaylineCoord(new PaylineCoord { ReelIndex = 3, Offset = 2 });
            payline3.AddPaylineCoord(new PaylineCoord { ReelIndex = 4, Offset = 2 });

            Payline payline4 = new Payline();
            payline4.AddPaylineCoord(new PaylineCoord { ReelIndex = 0, Offset = 0 });
            payline4.AddPaylineCoord(new PaylineCoord { ReelIndex = 1, Offset = 1 });
            payline4.AddPaylineCoord(new PaylineCoord { ReelIndex = 2, Offset = 2 });
            payline4.AddPaylineCoord(new PaylineCoord { ReelIndex = 3, Offset = 1 });
            payline4.AddPaylineCoord(new PaylineCoord { ReelIndex = 4, Offset = 0 });

            Payline payline5 = new Payline();
            payline5.AddPaylineCoord(new PaylineCoord { ReelIndex = 0, Offset = 2 });
            payline5.AddPaylineCoord(new PaylineCoord { ReelIndex = 1, Offset = 1 });
            payline5.AddPaylineCoord(new PaylineCoord { ReelIndex = 2, Offset = 0 });
            payline5.AddPaylineCoord(new PaylineCoord { ReelIndex = 3, Offset = 1 });
            payline5.AddPaylineCoord(new PaylineCoord { ReelIndex = 4, Offset = 2 });

            paylines.AddPayline(payline1);
            paylines.AddPayline(payline2);
            paylines.AddPayline(payline3);
            paylines.AddPayline(payline4);
            paylines.AddPayline(payline5);

            return paylines;
        }

        // 103%
        public override PayComboGroup BuildPayComboGroup()
        {
            PayComboGroup payCombos = new PayComboGroup();

            // AA
            payCombos.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 5, 50));
            payCombos.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 4, 25));
            payCombos.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 3, 10));

            // BB
            payCombos.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 5, 15));
            payCombos.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 4, 10));
            payCombos.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 3, 5));

            // CC
            payCombos.AddPayCombo(new PayCombo(new Symbol(2, "CC"), 5, 10));
            payCombos.AddPayCombo(new PayCombo(new Symbol(2, "CC"), 4, 5));
            payCombos.AddPayCombo(new PayCombo(new Symbol(2, "CC"), 3, 1));

            return payCombos;
        }
    }
}
