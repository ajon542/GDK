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

			ReelStrip reel2 = new ReelStrip ();
			reel2.AddSymbol (new Symbol (0, "AA"));
			reel2.AddSymbol (new Symbol (1, "BB"));

			ReelStrip reel3 = new ReelStrip ();
			reel3.AddSymbol (new Symbol (0, "AA"));
			reel3.AddSymbol (new Symbol (1, "BB"));

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

			paylines.AddPayline (payline1);

			return paylines;
		}

		public override PayComboGroup BuildPayComboGroup ()
		{
			PayComboGroup payCombos = new PayComboGroup ();

			payCombos.AddPayCombo (new PayCombo (new Symbol (0, "AA"), 3, 5));
			payCombos.AddPayCombo (new PayCombo (new Symbol (1, "BB"), 3, 2));

			return payCombos;
		}
	}
}
