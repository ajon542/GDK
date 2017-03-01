using System;
using System.Collections.Generic;
using GDK.MathEngine;

public class ScatterPaytableBuilder : PaytableBuilder
{
	public override ReelGroup BuildBGReelGroup ()
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
}