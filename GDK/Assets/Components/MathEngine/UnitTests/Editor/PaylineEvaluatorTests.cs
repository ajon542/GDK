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
    public void Init()
    {
        paylineEvaluator = new PaylineEvaluator();

        paytable = new Paytable();

        PaytableBuilder builder = new PaylinePaytableBuilder();
        paytable.BaseGameReelGroup = builder.BuildReelGroup();
        paytable.PaylineGroup = builder.BuildPaylineGroup();
        paytable.PayComboGroup = builder.BuildPayComboGroup();
        paytable.ScatterComboGroup = builder.BuildScatterComboGroup();
        paytable.PickTableGroup = builder.BuildPickTableGroup();
        paytable.PaytableTriggerGroup = builder.BuildPaytableTriggerGroup();

        rng = new Rng();
    }

    [Test]
    public void Evaluation_Initialization()
    {
        ReelWindow reelWindow = new ReelWindow(paytable.BaseGameReelGroup, new List<int> { 0, 0, 0 });

        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        Assert.IsNotNull(results);
    }

    [Test]
    public void Evaluation_SlotResult1()
    {
        // Reel Window
        // AA AA AA
        // BB BB BB
        // CC CC CC

        ReelWindow reelWindow = new ReelWindow(paytable.BaseGameReelGroup, new List<int> { 0, 0, 0 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(3, component.PayResults.Count);
        Assert.AreEqual(100, component.PayResults[0].PayCombo.PayAmount); // 3 x AA
        Assert.AreEqual(50, component.PayResults[1].PayCombo.PayAmount);  // 3 x BB
        Assert.AreEqual(20, component.PayResults[2].PayCombo.PayAmount);  // 3 x CC
    }

    [Test]
    public void Evaluation_SlotResult2()
    {
        // Reel Window
        // BB BB BB
        // CC CC CC
        // DD DD DD

        ReelWindow reelWindow = new ReelWindow(paytable.BaseGameReelGroup, new List<int> { 1, 1, 1 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);

        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(2, component.PayResults.Count);
        Assert.AreEqual(50, component.PayResults[0].PayCombo.PayAmount);  // 3 x BB
        Assert.AreEqual(20, component.PayResults[1].PayCombo.PayAmount);  // 3 x CC
    }

    [Test]
    public void Evaluation_SlotResult3()
    {
        // Reel Window
        // CC CC CC
        // DD DD DD
        // EE EE EE

        ReelWindow reelWindow = new ReelWindow(paytable.BaseGameReelGroup, new List<int> { 2, 2, 2 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);

        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);
        Assert.AreEqual(20, component.PayResults[0].PayCombo.PayAmount);  // 3 x CC
    }

    [Test]
    public void Evaluation_SlotResult4()
    {
        // Reel Window
        // GG GG GG
        // AA AA AA
        // BB BB BB

        ReelWindow reelWindow = new ReelWindow(paytable.BaseGameReelGroup, new List<int> { 6, 6, 6 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);

        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(2, component.PayResults.Count);
        Assert.AreEqual(100, component.PayResults[0].PayCombo.PayAmount); // 3 x AA
        Assert.AreEqual(50, component.PayResults[1].PayCombo.PayAmount);  // 3 x BB
    }

    [Test]
    public void Evaluation_SlotResult5()
    {
        // Reel Window
        // FF FF FF
        // GG GG GG
        // AA AA AA

        ReelWindow reelWindow = new ReelWindow(paytable.BaseGameReelGroup, new List<int> { 5, 5, 5 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);

        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);
        Assert.AreEqual(100, component.PayResults[0].PayCombo.PayAmount); // 3 x AA
    }
}
