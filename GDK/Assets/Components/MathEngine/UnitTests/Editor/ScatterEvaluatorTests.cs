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
		paytable = new Paytable ();
		scatterEvaluator = new ScatterEvaluator ();

		PaytableBuilder builder = new ScatterPaytableBuilder ();
		paytable.ReelGroup = builder.BuildReelGroup ();
		paytable.PaylineGroup = builder.BuildPaylineGroup ();
		paytable.PayComboGroup = builder.BuildPayComboGroup ();
		paytable.ScatterComboGroup = builder.BuildScatterComboGroup ();
		paytable.PickTableGroup = builder.BuildPickTableGroup ();
		paytable.PaytableTriggerGroup = builder.BuildPaytableTriggerGroup ();
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
		paytable.ScatterComboGroup.Combos.Clear ();
		paytable.ScatterComboGroup.AddPayCombo (new PayCombo(new Symbol(0, "AA"), 3, 1000));

		rng = new DummyRng (new List<int> { 0, 0, 0 });
		SlotResults results = scatterEvaluator.Evaluate (paytable, rng);
		Assert.AreEqual (1, results.Results.Count);
	}

	[Test]
	public void Evaluation_SlotResult2 ()
	{
		paytable.ScatterComboGroup.Combos.Clear ();
		paytable.ScatterComboGroup.AddPayCombo (new PayCombo(new Symbol(1, "BB"), 3, 1000));

		rng = new DummyRng (new List<int> { 0, 0, 0 });
		SlotResults results = scatterEvaluator.Evaluate (paytable, rng);
		Assert.AreEqual (1, results.Results.Count);
	}

	[Test]
	public void Evaluation_SlotResult3 ()
	{
		paytable.ScatterComboGroup.Combos.Clear ();
		paytable.ScatterComboGroup.AddPayCombo (new PayCombo(new Symbol(1, "BB"), 2, 1000));

		rng = new DummyRng (new List<int> { 0, 0, 0 });
		SlotResults results = scatterEvaluator.Evaluate (paytable, rng);
		Assert.AreEqual (1, results.Results.Count);
	}

	[Test]
	public void Evaluation_SlotResult4 ()
	{
		paytable.ScatterComboGroup.Combos.Clear ();
		paytable.ScatterComboGroup.AddPayCombo (new PayCombo(new Symbol(1, "BB"), 4, 1000));

		rng = new DummyRng (new List<int> { 0, 0, 0 });
		SlotResults results = scatterEvaluator.Evaluate (paytable, rng);
		Assert.AreEqual (0, results.Results.Count);
	}
}
