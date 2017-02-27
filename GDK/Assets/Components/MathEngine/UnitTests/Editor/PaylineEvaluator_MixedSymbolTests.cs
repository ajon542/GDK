using System.Collections.Generic;
using System.IO;
using GDK.MathEngine;
using GDK.MathEngine.Evaluators;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;


public class PaylineEvaluator_MixedSymbolTests
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
        ISymbolComparer comparer = new SymbolComparer();
        comparer.Substitute(new Symbol(0, "AA"), new Symbol(3, "MX"));
        comparer.Substitute(new Symbol(1, "BB"), new Symbol(3, "MX"));
        PayComboGroup payCombos = new PayComboGroup(comparer);

        // AA
        payCombos.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 5, 200));
        payCombos.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 4, 150));
        payCombos.AddPayCombo(new PayCombo(new Symbol(0, "AA"), 3, 100));

        // BB
        payCombos.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 5, 175));
        payCombos.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 4, 125));
        payCombos.AddPayCombo(new PayCombo(new Symbol(1, "BB"), 3, 85));

        // CC
        payCombos.AddPayCombo(new PayCombo(new Symbol(2, "CC"), 5, 130));
        payCombos.AddPayCombo(new PayCombo(new Symbol(2, "CC"), 4, 80));
        payCombos.AddPayCombo(new PayCombo(new Symbol(2, "CC"), 3, 30));

        // MX
        payCombos.AddPayCombo(new PayCombo(new Symbol(3, "MX"), 5, 44));
        payCombos.AddPayCombo(new PayCombo(new Symbol(3, "MX"), 4, 33));
        payCombos.AddPayCombo(new PayCombo(new Symbol(3, "MX"), 3, 22));

        paytable.ReelGroup = reels;
        paytable.PaylineGroup = paylines;
        paytable.PayComboGroup = payCombos;
    }

    [Test]
    public void Evaluation_PaylineMix1()
    {
        // Reel Window
        // AA BB AA BB AA
        // BB CC BB CC BB
        // CC AA CC AA CC

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 1, 0, 1, 0 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(1, component.PayResults.Count);

        Assert.AreEqual(44, component.PayResults[0].PayCombo.PayAmount);  // 5 x MX
    }

    [Test]
    public void Evaluation_PaylineMix2()
    {
        // Reel Window
        // AA AA AA AA AA
        // BB BB BB BB BB
        // CC CC CC CC CC

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 0, 0, 0, 0, 0 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);
        Assert.AreEqual(3, component.PayResults.Count);

        Assert.AreEqual(200, component.PayResults[0].PayCombo.PayAmount);  // 5 x AA
        Assert.AreEqual(175, component.PayResults[1].PayCombo.PayAmount);  // 4 x BB
        Assert.AreEqual(130, component.PayResults[2].PayCombo.PayAmount);  // 5 x CC
    }

    [Test]
    public void Evaluation_PaylineMix3()
    {
        // Reel Window
        // BB AA AA BB CC
        // CC BB BB CC AA
        // AA CC CC AA BB

        ReelWindow reelWindow = new ReelWindow(paytable.ReelGroup, new List<int> { 1, 0, 0, 1, 2 });
        SlotResult results = paylineEvaluator.Evaluate(paytable, reelWindow, rng);
        var component = results.GetComponent<PaylinesComponent>();
        Assert.IsNotNull(component);

        Assert.AreEqual(2, component.PayResults.Count);

        Assert.AreEqual(33, component.PayResults[0].PayCombo.PayAmount);  // 4 x MX (Payline 0)
        Assert.AreEqual(22, component.PayResults[1].PayCombo.PayAmount);  // 3 x MX (Payline 4)
    }
}
