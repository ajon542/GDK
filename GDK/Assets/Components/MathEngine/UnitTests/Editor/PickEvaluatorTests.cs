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
    public override PickTableGroup BuildPickTableGroup()
    {
        PickTableGroup pickTableGroup = new PickTableGroup();
        PickTable pickTable = new PickTable("Pick Feature");

        pickTable.PickItemList.Add(new PickItem { Name = "Prize_10", Value = 10 });
        pickTable.PickItemList.Add(new PickItem { Name = "Prize_10", Value = 10 });
        pickTable.PickItemList.Add(new PickItem { Name = "Prize_10", Value = 10 });
        pickTable.PickItemList.Add(new PickItem { Name = "Prize_10", Value = 10 });
        pickTable.PickItemList.Add(new PickItem { Name = "Prize_20", Value = 20 });
        pickTable.PickItemList.Add(new PickItem { Name = "Prize_20", Value = 20 });
        pickTable.PickItemList.Add(new PickItem { Name = "Prize_30", Value = 30 });
        pickTable.PickItemList.Add(new PickItem { Name = "PickComplete", Trigger = new PaytableTrigger { Name = "Free Spins" } });

        pickTableGroup.PickTable.Add(pickTable.Name, pickTable);
        return pickTableGroup;
    }
}

public class PickEvaluatorTests
{
    private IRng rng;
    private IEvaluator pickEvaluator;
    private Paytable paytable;

    [TestFixtureSetUp]
    public void Init()
    {
        PaytableBuilder builder = new PickPaytableBuilder();
        pickEvaluator = new PickEvaluator("Pick Feature");
        paytable = new Paytable();
        paytable.PickTableGroup = builder.BuildPickTableGroup();
    }

    [Test]
    public void Evaluation_SlotResult1()
    {
        rng = new DummyRng(new List<int> { 7 });

        SlotResults results = pickEvaluator.Evaluate(paytable, null, rng);

        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<PickComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PickResults.Count);
        Assert.AreEqual("Free Spins", component.PickResults[0].Trigger);
    }

    [Test]
    public void Evaluation_SlotResult2()
    {
        rng = new DummyRng(new List<int> { 6, 6 });
        SlotResults results = pickEvaluator.Evaluate(paytable, null,  rng);

        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<PickComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(2, component.PickResults.Count);
        Assert.AreEqual("Prize_30", component.PickResults[0].Name);
        Assert.AreEqual(30, component.PickResults[0].Value);
        Assert.AreEqual("Free Spins", component.PickResults[1].Trigger);
    }

    [Test]
    public void Evaluation_SlotResult3()
    {
        rng = new DummyRng(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 });
        SlotResults results = pickEvaluator.Evaluate(paytable, null,  rng);

        var component = results.Results[0].GetComponent<PickComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(8, component.PickResults.Count);
        Assert.AreEqual("Prize_10", component.PickResults[0].Name);
        Assert.AreEqual("Prize_10", component.PickResults[1].Name);
        Assert.AreEqual("Prize_10", component.PickResults[2].Name);
        Assert.AreEqual("Prize_10", component.PickResults[3].Name);
        Assert.AreEqual("Prize_20", component.PickResults[4].Name);
        Assert.AreEqual("Prize_20", component.PickResults[5].Name);
        Assert.AreEqual("Prize_30", component.PickResults[6].Name);
        Assert.AreEqual("PickComplete", component.PickResults[7].Name);
    }
}
