using System.Collections.Generic;
using System.IO;
using GDK.MathEngine;
using GDK.MathEngine.Evaluators;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Payline evaluation tests for a 5x3 reel window with 5 paylines.
/// </summary>
public class PaylineEvaluator_5_3_5_Tests
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

        Payline payline2 = new Payline();
        payline2.AddPaylineCoord(new PaylineCoord { ReelIndex = 0, Offset = 1 });
        payline2.AddPaylineCoord(new PaylineCoord { ReelIndex = 1, Offset = 1 });
        payline2.AddPaylineCoord(new PaylineCoord { ReelIndex = 2, Offset = 1 });
        payline2.AddPaylineCoord(new PaylineCoord { ReelIndex = 3, Offset = 1 });
        payline2.AddPaylineCoord(new PaylineCoord { ReelIndex = 4, Offset = 1 });

        Payline payline3 = new Payline();
        payline3.AddPaylineCoord(new PaylineCoord { ReelIndex = 0, Offset = 2 });
        payline3.AddPaylineCoord(new PaylineCoord { ReelIndex = 1, Offset = 2 });
        payline3.AddPaylineCoord(new PaylineCoord { ReelIndex = 2, Offset = 2 });
        payline3.AddPaylineCoord(new PaylineCoord { ReelIndex = 3, Offset = 2 });
        payline3.AddPaylineCoord(new PaylineCoord { ReelIndex = 4, Offset = 2 });

        Payline payline4 = new Payline();
        payline4.AddPaylineCoord(new PaylineCoord { ReelIndex = 0, Offset = 0 });
        payline4.AddPaylineCoord(new PaylineCoord { ReelIndex = 1, Offset = 1 });
        payline4.AddPaylineCoord(new PaylineCoord { ReelIndex = 2, Offset = 2 });
        payline4.AddPaylineCoord(new PaylineCoord { ReelIndex = 3, Offset = 1 });
        payline4.AddPaylineCoord(new PaylineCoord { ReelIndex = 4, Offset = 0 });

        Payline payline5 = new Payline();
        payline5.AddPaylineCoord(new PaylineCoord { ReelIndex = 0, Offset = 2 });
        payline5.AddPaylineCoord(new PaylineCoord { ReelIndex = 1, Offset = 1 });
        payline5.AddPaylineCoord(new PaylineCoord { ReelIndex = 2, Offset = 0 });
        payline5.AddPaylineCoord(new PaylineCoord { ReelIndex = 3, Offset = 1 });
        payline5.AddPaylineCoord(new PaylineCoord { ReelIndex = 4, Offset = 2 });

        paylines.AddPayline(payline1);
        paylines.AddPayline(payline2);
        paylines.AddPayline(payline3);
        paylines.AddPayline(payline4);
        paylines.AddPayline(payline5);

        // PayCombos
        PayComboGroup payCombos = new PayComboGroup(new SymbolComparer());

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
        SlotResults results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        Assert.IsNotNull(results);
    }

    [Test]
    public void Evaluation_Payline_1_2_3()
    {
        // Reel Window
        // AA AA AA AA AA
        // BB BB BB BB BB
        // CC CC CC CC CC

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0, 0, 0 });
        SlotResults results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(3, component.PayResults.Count);

        Assert.AreEqual(50, component.PayResults[0].PayCombo.PayAmount);  // 5 x AA
        Assert.AreEqual(15, component.PayResults[1].PayCombo.PayAmount);  // 5 x BB
        Assert.AreEqual(10, component.PayResults[2].PayCombo.PayAmount);  // 5 x CC
    }

    [Test]
    public void Evaluation_Payline_4()
    {
        // Reel Window
        // AA CC BB CC AA
        // BB AA CC AA BB
        // CC BB AA CC CC

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 2, 1, 2, 0 });
        SlotResults results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);

        Assert.AreEqual(50, component.PayResults[0].PayCombo.PayAmount);  // 5 x AA
    }

    [Test]
    public void Evaluation_Payline_5()
    {
        // Reel Window
        // BB CC AA CC BB
        // CC AA BB AA CC
        // AA BB CC BB AA

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 1, 2, 0, 2, 1 });
        SlotResults results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);

        Assert.AreEqual(50, component.PayResults[0].PayCombo.PayAmount);  // 5 x AA
    }

    [Test]
    public void Evaluation_Payline_4_3_of_a_kind()
    {
        // Reel Window
        // AA CC BB BB BB
        // CC AA CC CC CC
        // AA BB AA AA AA

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 2, 1, 1, 1 });
        SlotResults results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);

        Assert.AreEqual(10, component.PayResults[0].PayCombo.PayAmount);  // 3 x AA
    }

    [Test]
    public void Evaluation_Payline_4_4_of_a_kind()
    {
        // Reel Window
        // AA CC BB CC BB
        // CC AA CC AA CC
        // AA BB AA BB AA

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 2, 1, 2, 1 });
        SlotResults results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);

        Assert.AreEqual(25, component.PayResults[0].PayCombo.PayAmount);  // 4 x AA
    }

    [Test]
    public void Evaluation_Payline_All_3_of_a_kind()
    {
        // Reel Window
        // AA AA AA CC BB
        // BB BB BB AA CC
        // CC CC CC BB AA

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0, 2, 1 });
        SlotResults results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(3, component.PayResults.Count);

        Assert.AreEqual(10, component.PayResults[0].PayCombo.PayAmount);  // 3 x AA
        Assert.AreEqual( 5, component.PayResults[1].PayCombo.PayAmount);  // 3 x BB
        Assert.AreEqual( 1, component.PayResults[2].PayCombo.PayAmount);  // 3 x CC
    }
}
