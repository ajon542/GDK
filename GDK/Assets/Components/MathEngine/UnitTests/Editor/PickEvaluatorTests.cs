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
	public void EditorTest ()
	{
	}
}
