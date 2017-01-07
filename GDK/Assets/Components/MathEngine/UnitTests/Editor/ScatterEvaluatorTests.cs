using System.Collections.Generic;
using System.IO;
using GDK.MathEngine;
using GDK.MathEngine.Evaluators;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class ScatterEvaluatorTests
{
	private IRng rng;
	private IEvaluator scatterEvaluator;
	private Paytable paytable;

	[TestFixtureSetUp]
	public void Init ()
	{
		DummyPaytableGenerator generator = new DummyPaytableGenerator ();
		paytable = generator.Generate ();

		scatterEvaluator = new ScatterEvaluator ();
	}

	[Test]
	public void Evaluation_Initialization ()
	{
		rng = new DummyRng (new List<int> { 0, 0, 0 });
		SlotResults results = scatterEvaluator.Evaluate (paytable, rng);
		Assert.IsNotNull (results);
	}

	[Test]
	public void Evaluation_SlotResult1 ()
	{
		// Reel Window
		// AA AA AA
		// BB BB BB
		// CC CC CC

		ReelGroup reels = new ReelGroup ();

		ReelStrip reel1 = new ReelStrip ();
		reel1.AddSymbol (new Symbol (0, "AA"));
		reel1.AddSymbol (new Symbol (1, "BB"));
		reel1.AddSymbol (new Symbol (2, "CC"));

		ReelStrip reel2 = new ReelStrip ();
		reel2.AddSymbol (new Symbol (3, "DD"));
		reel2.AddSymbol (new Symbol (4, "EE"));
		reel2.AddSymbol (new Symbol (5, "FF"));

		ReelStrip reel3 = new ReelStrip ();
		reel3.AddSymbol (new Symbol (6, "GG"));
		reel3.AddSymbol (new Symbol (7, "HH"));
		reel3.AddSymbol (new Symbol (8, "II"));

		reels.AddReel (reel1);
		reels.AddReel (reel2);
		reels.AddReel (reel3);

		paytable.ReelGroup = reels;
		paytable.ScatterComboGroup.Combos.Clear ();
		paytable.ScatterComboGroup.AddPayCombo (new PayCombo(new Symbol(0, "AA"), 3, 1000));

		rng = new DummyRng (new List<int> { 0, 0, 0 });
		SlotResults results = scatterEvaluator.Evaluate (paytable, rng);
		Assert.AreEqual (0, results.Results.Count);
	}
}
