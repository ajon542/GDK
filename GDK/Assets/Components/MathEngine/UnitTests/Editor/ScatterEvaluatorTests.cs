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
    public void Init()
    {
        paytable = new Paytable();
        scatterEvaluator = new ScatterEvaluator();
        rng = new Rng();

        PaytableBuilder builder = new ScatterPaytableBuilder();
        paytable.ReelGroup = builder.BuildReelGroup();
        paytable.PaylineGroup = builder.BuildPaylineGroup();
        paytable.PayComboGroup = builder.BuildPayComboGroup();
        paytable.ScatterComboGroup = builder.BuildScatterComboGroup();
        paytable.PickTableGroup = builder.BuildPickTableGroup();
        paytable.PaytableTriggerGroup = builder.BuildPaytableTriggerGroup();
    }

    [Test]
    public void Evaluation_Initialization()
    {
        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0 });
        SlotResults results = scatterEvaluator.Evaluate(paytable, reelWindow, rng);
        Assert.IsNotNull(results);
    }

    [Test]
    public void Evaluation_SlotResult1()
    {
        paytable.ScatterComboGroup.Combos.Clear();
        paytable.ScatterComboGroup.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 3, 1000));

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0 });
        SlotResults results = scatterEvaluator.Evaluate(paytable, reelWindow, rng);

        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<ScattersComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);
        Assert.AreEqual(1000, component.PayResults[0].PayCombo.PayAmount);
    }

    [Test]
    public void Evaluation_SlotResult2()
    {
        paytable.ScatterComboGroup.Combos.Clear();
        paytable.ScatterComboGroup.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 3, 1000));

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0 });
        SlotResults results = scatterEvaluator.Evaluate(paytable, reelWindow, rng);

        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<ScattersComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);
        Assert.AreEqual(1000, component.PayResults[0].PayCombo.PayAmount);
    }

    [Test]
    public void Evaluation_SlotResult3()
    {
        paytable.ScatterComboGroup.Combos.Clear();
        paytable.ScatterComboGroup.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 2, 1000));

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0 });
        SlotResults results = scatterEvaluator.Evaluate(paytable, reelWindow, rng);

        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<ScattersComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);
        Assert.AreEqual(1000, component.PayResults[0].PayCombo.PayAmount);
    }

    [Test]
    public void Evaluation_SlotResult4()
    {
        paytable.ScatterComboGroup.Combos.Clear();
        paytable.ScatterComboGroup.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 4, 1000));

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0 });
        SlotResults results = scatterEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.Results[0].GetComponent<ScattersComponent>();
        Assert.IsNull(component);
    }

    [Test]
    public void Evaluation_SlotResult5()
    {
        paytable.ScatterComboGroup.Combos.Clear();
        paytable.ScatterComboGroup.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 3, 1000));
        paytable.ScatterComboGroup.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 3, 500));
        paytable.ScatterComboGroup.AddPayCombo(new PayCombo(new Symbol(2, "CC"), 3, 10));

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0 });
        SlotResults results = scatterEvaluator.Evaluate(paytable, reelWindow, rng);

        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<ScattersComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(3, component.PayResults.Count);
        Assert.AreEqual(1000, component.PayResults[0].PayCombo.PayAmount);
        Assert.AreEqual(500, component.PayResults[1].PayCombo.PayAmount);
        Assert.AreEqual(10, component.PayResults[2].PayCombo.PayAmount);
    }

    [Test]
    public void Evaluation_SlotResult6()
    {
        paytable.ScatterComboGroup.Combos.Clear();
        paytable.ScatterComboGroup.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 1, 10));
        paytable.ScatterComboGroup.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 2, 100));

        // TODO: The requirements for this haven't been set.
        // These are two separate PayCombos so it is matching correctly. However, I think
        // we're expecting something like the best of 3xAA, 4xAA or 5xAA

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0 });
        SlotResults results = scatterEvaluator.Evaluate(paytable, reelWindow, rng);
        Assert.AreEqual(1, results.Results.Count);
        //Assert.AreEqual (100, results.Results [0].TotalValue);
    }

    [Test]
    public void Evaluation_SlotResult7()
    {
        paytable.ScatterComboGroup.Combos.Clear();
        paytable.ScatterComboGroup.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 3, 100, "Free Spins"));

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0 });
        SlotResults results = scatterEvaluator.Evaluate(paytable, reelWindow, rng);

        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<ScattersComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);
        Assert.AreEqual("Free Spins", component.PayResults[0].PayCombo.Trigger);
    }

    [Test]
    public void Evaluation_SlotResult8()
    {
        paytable.ScatterComboGroup.Combos.Clear();
        paytable.ScatterComboGroup.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 3, 100, "Free Spins"));

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0 });
        SlotResults results = scatterEvaluator.Evaluate(paytable, reelWindow, rng);

        var component = results.Results[0].GetComponent<ScattersComponent>();
        Payline payline = component.PayResults[0].Payline;

        Assert.AreEqual(0, payline.PaylineCoords[0].ReelIndex);
        Assert.AreEqual(0, payline.PaylineCoords[0].Offset);
        Assert.AreEqual(1, payline.PaylineCoords[1].ReelIndex);
        Assert.AreEqual(0, payline.PaylineCoords[1].Offset);
        Assert.AreEqual(2, payline.PaylineCoords[2].ReelIndex);
        Assert.AreEqual(0, payline.PaylineCoords[2].Offset);
    }
}
