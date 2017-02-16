using System.Collections.Generic;
using System.IO;
using GDK.MathEngine;
using GDK.MathEngine.Evaluators;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Payline evaluation tests for a 5x3 reel window with 3 paylines.
/// </summary>
public class PaylineEvaluator_5_3_3_Tests
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

        paylines.AddPayline(payline1);
        paylines.AddPayline(payline2);
        paylines.AddPayline(payline3);

        // PayCombos
        PayComboGroup payCombos = new PayComboGroup();

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
        rng = new DummyRng(new List<int> { 0, 0, 0, 0, 0 });
        SlotResults results = paylineEvaluator.Evaluate(paytable, rng);
        Assert.IsNotNull(results);
    }


    [Test]
    public void Evaluation_SlotResult1()
    {
        // Reel Window
        // AA AA AA AA AA
        // BB BB BB BB BB
        // CC CC CC CC CC

        rng = new DummyRng(new List<int> { 0, 0, 0, 0, 0 });
        SlotResults results = paylineEvaluator.Evaluate(paytable, rng);
        Assert.AreEqual(1, results.Results.Count);
        var component = results.Results[0].GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(3, component.PayResults.Count);

        Assert.AreEqual(50, component.PayResults[0].PayCombo.PayAmount);  // 5 x AA
        Assert.AreEqual(15, component.PayResults[1].PayCombo.PayAmount);  // 5 x BB
        Assert.AreEqual(10, component.PayResults[2].PayCombo.PayAmount);  // 5 x CC
    }
}
