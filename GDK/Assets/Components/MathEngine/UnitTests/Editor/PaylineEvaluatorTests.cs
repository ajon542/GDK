using System.Collections.Generic;
using System.IO;
using GDK.MathEngine;
using GDK.MathEngine.Evaluators;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class PaylineEvaluatorTests
{
	private IRng rng;
	private IEvaluator paylineEvaluator;
	private Paytable paytable;

	[TestFixtureSetUp]
	public void Init ()
	{
		paylineEvaluator = new PaylineEvaluator ();

		paytable = new DummyPaytableGenerator ().Generate ();

		string json = JsonConvert.SerializeObject(paytable, Formatting.Indented);
		File.WriteAllText ("/Users/andrew/development/logs/Paytable.txt", json);

		//string result = File.ReadAllText ("Assets/Components/MathEngine/UnitTests/Data/PaylineEvaluatorTest-Paytable.txt");
		//paytable = JsonConvert.DeserializeObject<Paytable> (result);
		paytable = new Paytable();

		ReelGroup reels = new ReelGroup ();

		// Create the reels.
		ReelStrip reel1 = new ReelStrip ();
		reel1.AddSymbol (new Symbol (0, "AA"));
		reel1.AddSymbol (new Symbol (1, "BB"));
		reel1.AddSymbol (new Symbol (2, "CC"));
		reel1.AddSymbol (new Symbol (3, "DD"));
		reel1.AddSymbol (new Symbol (4, "EE"));
		reel1.AddSymbol (new Symbol (5, "FF"));
		reel1.AddSymbol (new Symbol (6, "GG"));

		ReelStrip reel2 = new ReelStrip ();
		reel2.AddSymbol (new Symbol (0, "AA"));
		reel2.AddSymbol (new Symbol (1, "BB"));
		reel2.AddSymbol (new Symbol (2, "CC"));
		reel2.AddSymbol (new Symbol (3, "DD"));
		reel2.AddSymbol (new Symbol (4, "EE"));
		reel2.AddSymbol (new Symbol (5, "FF"));
		reel2.AddSymbol (new Symbol (6, "GG"));

		ReelStrip reel3 = new ReelStrip ();
		reel3.AddSymbol (new Symbol (0, "AA"));
		reel3.AddSymbol (new Symbol (1, "BB"));
		reel3.AddSymbol (new Symbol (2, "CC"));
		reel3.AddSymbol (new Symbol (3, "DD"));
		reel3.AddSymbol (new Symbol (4, "EE"));
		reel3.AddSymbol (new Symbol (5, "FF"));
		reel3.AddSymbol (new Symbol (6, "GG"));

		reels.AddReel (reel1);
		reels.AddReel (reel2);
		reels.AddReel (reel3);

		// Create the paylines.
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

		// Create the pay combos.
		PayComboGroup payCombos = new PayComboGroup ();

		// TODO: This is where we might want to add the triggers.
		payCombos.AddPayCombo (new PayCombo (new Symbol (0, "AA"), 2, 10));
		payCombos.AddPayCombo (new PayCombo (new Symbol (0, "AA"), 3, 100));
		payCombos.AddPayCombo (new PayCombo (new Symbol (1, "BB"), 3, 50));
		payCombos.AddPayCombo (new PayCombo (new Symbol (2, "CC"), 3, 20));

		paytable.ReelGroup = reels;
		paytable.PaylineGroup = paylines;
		paytable.PayComboGroup = payCombos;
	}

	[Test]
	public void Evaluation_Initialization ()
	{
		rng = new DummyRng (new List<int> { 0, 0, 0 });
		SlotResults results = paylineEvaluator.Evaluate (paytable, rng);
		Assert.IsNotNull (results);
	}

	[Test]
	public void Evaluation_SlotResult1 ()
	{
		// Reel Window
		// AA AA AA
		// BB BB BB
		// CC CC CC

		rng = new DummyRng (new List<int> { 0, 0, 0 });
		SlotResults results = paylineEvaluator.Evaluate (paytable, rng);
		Assert.AreEqual (3, results.Results.Count);

		Assert.AreEqual (100, (results.Results [0] as SlotResult).PayCombo.PayAmount); // 3 x AA
		Assert.AreEqual (50, (results.Results [1] as SlotResult).PayCombo.PayAmount);  // 3 x BB
		Assert.AreEqual (20, (results.Results [2] as SlotResult).PayCombo.PayAmount);  // 3 x CC
	}

	[Test]
	public void Evaluation_SlotResult2 ()
	{
		// Reel Window
		// BB BB BB
		// CC CC CC
		// DD DD DD

		rng = new DummyRng (new List<int> { 1, 1, 1 });
		SlotResults results = paylineEvaluator.Evaluate (paytable, rng);
		Assert.AreEqual (2, results.Results.Count);

		Assert.AreEqual (50, (results.Results [0] as SlotResult).PayCombo.PayAmount);  // 3 x BB
		Assert.AreEqual (20, (results.Results [1] as SlotResult).PayCombo.PayAmount);  // 3 x CC
	}

	[Test]
	public void Evaluation_SlotResult3 ()
	{
		// Reel Window
		// CC CC CC
		// DD DD DD
		// EE EE EE

		rng = new DummyRng (new List<int> { 2, 2, 2 });
		SlotResults results = paylineEvaluator.Evaluate (paytable, rng);
		Assert.AreEqual (1, results.Results.Count);

		Assert.AreEqual (20, (results.Results [0] as SlotResult).PayCombo.PayAmount);  // 3 x CC
	}

	[Test]
	public void Evaluation_SlotResult4 ()
	{
		// Reel Window
		// GG GG GG
		// AA AA AA
		// BB BB BB

		rng = new DummyRng (new List<int> { 6, 6, 6 });
		SlotResults results = paylineEvaluator.Evaluate (paytable, rng);
		Assert.AreEqual (2, results.Results.Count);

		Assert.AreEqual (100, (results.Results [0] as SlotResult).PayCombo.PayAmount);  // 3 x AA
		Assert.AreEqual (50, (results.Results [1] as SlotResult).PayCombo.PayAmount);   // 3 x BB
	}

	[Test]
	public void Evaluation_SlotResult5 ()
	{
		// Reel Window
		// FF FF FF
		// GG GG GG
		// AA AA AA

		rng = new DummyRng (new List<int> { 5, 5, 5 });
		SlotResults results = paylineEvaluator.Evaluate (paytable, rng);
		Assert.AreEqual (1, results.Results.Count);

		Assert.AreEqual (100, (results.Results [0] as SlotResult).PayCombo.PayAmount);  // 3 x AA
	}
}
