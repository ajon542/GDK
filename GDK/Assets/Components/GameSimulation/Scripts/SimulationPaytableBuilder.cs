using UnityEngine;
using System.Collections;

using GDK.MathEngine;

namespace GDK.GameSimulation
{
	public class SimulationPaytableBuilder : PaytableBuilder
	{
		public override ReelGroup BuildReelGroup ()
		{
			ReelGroup reels = new ReelGroup ();

			ReelStrip reel1 = new ReelStrip ();
			reel1.AddSymbol (new Symbol (0, "AA"));
			reel1.AddSymbol (new Symbol (1, "BB"));
			reel1.AddSymbol (new Symbol (2, "CC"));

			ReelStrip reel2 = new ReelStrip ();
			reel2.AddSymbol (new Symbol (0, "AA"));
			reel2.AddSymbol (new Symbol (1, "BB"));
			reel2.AddSymbol (new Symbol (2, "CC"));

			ReelStrip reel3 = new ReelStrip ();
			reel3.AddSymbol (new Symbol (0, "AA"));
			reel3.AddSymbol (new Symbol (1, "BB"));
			reel3.AddSymbol (new Symbol (2, "CC"));

			reels.AddReel (reel1);
			reels.AddReel (reel2);
			reels.AddReel (reel3);

			return reels;
		}

		public override PaylineGroup BuildPaylineGroup ()
		{
			PaylineGroup paylines = new PaylineGroup ();

			Payline payline1 = new Payline ();
			payline1.AddPaylineCoord (new PaylineCoord { ReelIndex = 0, Offset = 0 });
			payline1.AddPaylineCoord (new PaylineCoord { ReelIndex = 1, Offset = 0 });
			payline1.AddPaylineCoord (new PaylineCoord { ReelIndex = 2, Offset = 0 });

			Payline payline2 = new Payline ();
			payline2.AddPaylineCoord (new PaylineCoord { ReelIndex = 0, Offset = 1 });
			payline2.AddPaylineCoord (new PaylineCoord { ReelIndex = 1, Offset = 1 });
			payline2.AddPaylineCoord (new PaylineCoord { ReelIndex = 2, Offset = 1 });

			Payline payline3 = new Payline ();
			payline3.AddPaylineCoord (new PaylineCoord { ReelIndex = 0, Offset = 2 });
			payline3.AddPaylineCoord (new PaylineCoord { ReelIndex = 1, Offset = 2 });
			payline3.AddPaylineCoord (new PaylineCoord { ReelIndex = 2, Offset = 2 });

			paylines.AddPayline (payline1);
			paylines.AddPayline (payline2);
			paylines.AddPayline (payline3);

			return paylines;
		}

		// 29%
		public override PayComboGroup BuildPayComboGroup ()
		{
			PayComboGroup payCombos = new PayComboGroup ();

			payCombos.AddPayCombo (new PayCombo (new Symbol (0, "AA"), 3, 5));
			payCombos.AddPayCombo (new PayCombo (new Symbol (1, "BB"), 3, 2));
			payCombos.AddPayCombo (new PayCombo (new Symbol (2, "CC"), 3, 1));

			return payCombos;
		}
	}
}
