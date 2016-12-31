using System.Collections.Generic;
using GDK.MathEngine;
using GDK.MathEngine.Evaluator;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class EvaluationTests
{
	private IRng rng;
	private IEvaluator paytableEvaluator;
	private IPaytableGenerator paytableGenerator;

	[TestFixtureSetUp]
	public void Init ()
	{
		paytableEvaluator = new Evaluator ();
		paytableGenerator = new DummyPaytableGenerator ();
	}

	[Test]
	public void Evaluation_Initialization ()
	{
		rng = new DummyRng (new List<int> { 0, 0, 0 });
		SlotResults results = paytableEvaluator.Evaluate (paytableGenerator.Generate (), rng);
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
		SlotResults results = paytableEvaluator.Evaluate (paytableGenerator.Generate (), rng);
		Assert.AreEqual (3, results.Results.Count);

		Assert.AreEqual (100, results.Results[0].PayCombo.PayAmount); // 3 x AA
		Assert.AreEqual (50, results.Results[1].PayCombo.PayAmount);  // 3 x BB
		Assert.AreEqual (20, results.Results[2].PayCombo.PayAmount);  // 3 x CC
	}

	[Test]
	public void Evaluation_SlotResult2 ()
	{
		// Reel Window
		// BB BB BB
		// CC CC CC
		// DD DD DD

		rng = new DummyRng (new List<int> { 1, 1, 1 });
		SlotResults results = paytableEvaluator.Evaluate (paytableGenerator.Generate (), rng);
		Assert.AreEqual (2, results.Results.Count);

		Assert.AreEqual (50, results.Results[0].PayCombo.PayAmount);  // 3 x BB
		Assert.AreEqual (20, results.Results[1].PayCombo.PayAmount);  // 3 x CC
	}

	[Test]
	public void Evaluation_SlotResult3 ()
	{
		// Reel Window
		// CC CC CC
		// DD DD DD
		// EE EE EE

		rng = new DummyRng (new List<int> { 2, 2, 2 });
		SlotResults results = paytableEvaluator.Evaluate (paytableGenerator.Generate (), rng);
		Assert.AreEqual (1, results.Results.Count);

		Assert.AreEqual (20, results.Results[0].PayCombo.PayAmount);  // 3 x CC
	}

	[Test]
	public void Evaluation_SlotResult4 ()
	{
		// Reel Window
		// GG GG GG
		// AA AA AA
		// BB BB BB

		rng = new DummyRng (new List<int> { 6, 6, 6 });
		SlotResults results = paytableEvaluator.Evaluate (paytableGenerator.Generate (), rng);
		Assert.AreEqual (2, results.Results.Count);

		Assert.AreEqual (100, results.Results[0].PayCombo.PayAmount);  // 3 x AA
		Assert.AreEqual (50, results.Results[1].PayCombo.PayAmount);   // 3 x BB
	}

	[Test]
	public void Evaluation_SlotResult5 ()
	{
		// Reel Window
		// FF FF FF
		// GG GG GG
		// AA AA AA

		rng = new DummyRng (new List<int> { 5, 5, 5 });
		SlotResults results = paytableEvaluator.Evaluate (paytableGenerator.Generate (), rng);
		Assert.AreEqual (1, results.Results.Count);

		Assert.AreEqual (100, results.Results[0].PayCombo.PayAmount);  // 3 x AA
	}
}
