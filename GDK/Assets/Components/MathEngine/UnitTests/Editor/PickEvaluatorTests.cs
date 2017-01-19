using System.Collections.Generic;
using System.IO;
using GDK.MathEngine;
using GDK.MathEngine.Evaluators;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class PickPaytableBuilder : PaytableBuilder
{
	public override PickTableGroup BuildPickTableGroup ()
	{
		PickTableGroup pickTableGroup = new PickTableGroup ();
		PickTable pickTable = new PickTable ("Pick Feature");

		pickTable.PickItemList.Add (new PickItem { Name = "Prize_10", Value = 10 });
		pickTable.PickItemList.Add (new PickItem { Name = "Prize_10", Value = 10 });
		pickTable.PickItemList.Add (new PickItem { Name = "Prize_10", Value = 10 });
		pickTable.PickItemList.Add (new PickItem { Name = "Prize_10", Value = 10 });
		pickTable.PickItemList.Add (new PickItem { Name = "Prize_20", Value = 20 });
		pickTable.PickItemList.Add (new PickItem { Name = "Prize_20", Value = 20 });
		pickTable.PickItemList.Add (new PickItem { Name = "Prize_30", Value = 30 });
		pickTable.PickItemList.Add (new PickItem { Name = "PickComplete", Trigger = new PaytableTrigger { Name = "Free Spins" } });

		pickTableGroup.PickTable.Add (pickTable.Name, pickTable);
		return pickTableGroup;
	}
}

public class PickEvaluatorTests
{
	private IRng rng;
	private IEvaluator pickEvaluator;
	private Paytable paytable;

	[TestFixtureSetUp]
	public void Init ()
	{
		PaytableBuilder builder = new PickPaytableBuilder ();
		pickEvaluator = new PickEvaluator ("Pick Feature");
		paytable = new Paytable ();
		paytable.PickTableGroup = builder.BuildPickTableGroup ();
	}

	[Test]
	public void Evaluation_SlotResult1 ()
	{
		rng = new DummyRng (new List<int> { 7 });
		SlotResults results = pickEvaluator.Evaluate (paytable, rng);

		Assert.AreEqual (1, results.Results.Count);
		Assert.AreEqual ("Free Spins", results.Results[0].PickResult.Trigger);
	}

	[Test]
	public void Evaluation_SlotResult2 ()
	{
		rng = new DummyRng (new List<int> { 6, 6 });
		SlotResults results = pickEvaluator.Evaluate (paytable, rng);

		Assert.AreEqual (2, results.Results.Count);
		Assert.AreEqual ("Prize_30", results.Results[0].PickResult.Name);
		Assert.AreEqual (30, results.Results[0].PickResult.Value);
		Assert.AreEqual ("Free Spins", results.Results[1].PickResult.Trigger);
	}

	[Test]
	public void Evaluation_SlotResult3 ()
	{
		rng = new DummyRng (new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 });
		SlotResults results = pickEvaluator.Evaluate (paytable, rng);

		Assert.AreEqual (8, results.Results.Count);
		Assert.AreEqual ("Prize_10", results.Results[0].PickResult.Name);
		Assert.AreEqual ("Prize_10", results.Results[1].PickResult.Name);
		Assert.AreEqual ("Prize_10", results.Results[2].PickResult.Name);
		Assert.AreEqual ("Prize_10", results.Results[3].PickResult.Name);
		Assert.AreEqual ("Prize_20", results.Results[4].PickResult.Name);
		Assert.AreEqual ("Prize_20", results.Results[5].PickResult.Name);
		Assert.AreEqual ("Prize_30", results.Results[6].PickResult.Name);
		Assert.AreEqual ("PickComplete", results.Results[7].PickResult.Name);
	}
}
