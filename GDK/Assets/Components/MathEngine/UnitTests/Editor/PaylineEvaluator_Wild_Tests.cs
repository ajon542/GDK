using System.Collections.Generic;
using System.IO;
using GDK.MathEngine;
using GDK.MathEngine.Evaluators;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Payline evaluation Wild symbol tests for a 5x3 reel window with 5 paylines.
/// </summary>
public class PaylineEvaluator_Wild_Tests
{
    private IRng rng;
    private IEvaluator paylineEvaluator;
    private Paytable paytable;

    [TestFixtureSetUp]
    public void Init()
    {
        rng = new Rng();
        paylineEvaluator = new PaylineEvaluator();
        paytable = new Paytable();

        // Reels
        ReelGroup reels = new ReelGroup();

        ReelStrip reel = new ReelStrip();
        reel.AddSymbol(new Symbol(0, "AA"));
        reel.AddSymbol(new Symbol(1, "BB"));
        reel.AddSymbol(new Symbol(2, "CC"));
        reel.AddSymbol(new Symbol(3, "WW"));

        reels.AddReel(reel);
        reels.AddReel(reel);
        reels.AddReel(reel);
        reels.AddReel(reel);
        reels.AddReel(reel);

        // Paylines
        PaylineGroup paylines = new PaylineGroup();

        Payline payline1 = new Payline();
        payline1.AddPaylineCoord(new PaylineCoord { ReelIndex = 0, Offset = 0 });
        payline1.AddPaylineCoord(new PaylineCoord { ReelIndex = 1, Offset = 0 });
        payline1.AddPaylineCoord(new PaylineCoord { ReelIndex = 2, Offset = 0 });
        payline1.AddPaylineCoord(new PaylineCoord { ReelIndex = 3, Offset = 0 });
        payline1.AddPaylineCoord(new PaylineCoord { ReelIndex = 4, Offset = 0 });

        paylines.AddPayline(payline1);

        // PayCombos
        ISymbolComparer comparer = new SymbolComparer();
        comparer.Substitute(new Symbol(3, "WW"), new Symbol(0, "AA"));
        comparer.Substitute(new Symbol(3, "WW"), new Symbol(0, "BB"));
        comparer.Substitute(new Symbol(3, "WW"), new Symbol(0, "CC"));
        PayComboGroup payCombos = new PayComboGroup(comparer);

        // WW
        payCombos.AddPayCombo(new PayCombo(new Symbol(3, "WW"), 5, 1000));
        payCombos.AddPayCombo(new PayCombo(new Symbol(3, "WW"), 4, 500));
        payCombos.AddPayCombo(new PayCombo(new Symbol(3, "WW"), 3, 100));

        // AA
        payCombos.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 5, 50));
        payCombos.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 4, 25));
        payCombos.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 3, 10));

        // BB
        payCombos.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 5, 15));
        payCombos.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 4, 10));
        payCombos.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 3, 5));

        // CC
        payCombos.AddPayCombo(new PayCombo(new Symbol(2, "CC"), 5, 10));
        payCombos.AddPayCombo(new PayCombo(new Symbol(2, "CC"), 4, 5));
        payCombos.AddPayCombo(new PayCombo(new Symbol(2, "CC"), 3, 1));

        paytable.ReelGroup = reels;
        paytable.PaylineGroup = paylines;
        paytable.PayComboGroup = payCombos;
    }

    [Test]
    public void Evaluation_Initialization()
    {
        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0, 0, 0 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        Assert.IsNotNull(results);
    }

    [Test]
    public void Evaluation_Payline1()
    {
        // Reel Window
        // WW AA AA AA AA
        // AA BB BB BB BB
        // BB CC CC CC CC

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 3, 0, 0, 0, 0 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);

        Assert.AreEqual(50, component.PayResults[0].PayCombo.PayAmount);  // 5 x AA
    }

    [Test]
    public void Evaluation_Payline2()
    {
        // Reel Window
        // WW AA AA AA BB
        // AA BB BB BB CC
        // BB CC CC CC AA

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 3, 0, 0, 0, 1 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);

        Assert.AreEqual(25, component.PayResults[0].PayCombo.PayAmount);  // 4 x AA
    }

    [Test]
    public void Evaluation_Payline3()
    {
        // Reel Window
        // WW AA WW AA BB
        // AA BB AA BB CC
        // BB CC BB CC WW

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 3, 0, 3, 0, 1 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);

        Assert.AreEqual(25, component.PayResults[0].PayCombo.PayAmount);  // 4 x AA
    }

    [Test]
    public void Evaluation_Payline4()
    {
        // Reel Window
        // AA AA WW BB BB
        // BB BB AA CC CC
        // CC CC BB WW WW

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 3, 1, 1 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);

        Assert.AreEqual(10, component.PayResults[0].PayCombo.PayAmount);  // 3 x AA
    }

    [Test]
    public void Evaluation_Payline5()
    {
        // Reel Window
        // WW WW WW WW WW
        // AA AA AA AA AA
        // BB BB BB BB BB

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 3, 3, 3, 3, 3 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);

        Assert.AreEqual(1000, component.PayResults[0].PayCombo.PayAmount);  // 5 x WW
    }

    [Test]
    public void Evaluation_Payline6()
    {
        // Reel Window
        // WW WW WW WW AA
        // AA AA AA AA BB
        // BB BB BB BB CC

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 3, 3, 3, 3, 0 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);

        Assert.AreEqual(500, component.PayResults[0].PayCombo.PayAmount);  // 4 x WW
    }

    [Test]
    public void Evaluation_Payline7()
    {
        // Reel Window
        // WW WW WW AA AA
        // AA AA AA BB BB
        // BB BB BB CC CC

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 3, 3, 3, 0, 0 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);

        Assert.AreEqual(100, component.PayResults[0].PayCombo.PayAmount);  // 3 x WW
    }

    [Test]
    public void Evaluation_Payline8()
    {
        // Reel Window
        // WW AA BB AA AA
        // AA AA CC BB BB
        // BB CC WW CC CC

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 3, 0, 1, 0, 0 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNull(component);
    }
}
