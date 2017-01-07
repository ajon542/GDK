using System.Collections.Generic;
using System.IO;
using GDK.MathEngine;
using GDK.MathEngine.Evaluators;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class ReelWindowTests
{
	private ReelGroup reels;

	[Test]
	public void ReelWindow_LessThanDefaultHeight ()
	{
		reels = new ReelGroup ();

		ReelStrip reel1 = new ReelStrip ();
		reel1.AddSymbol (new Symbol (0, "AA"));

		ReelStrip reel2 = new ReelStrip ();
		reel2.AddSymbol (new Symbol (1, "BB"));

		ReelStrip reel3 = new ReelStrip ();
		reel3.AddSymbol (new Symbol (2, "CC"));

		reels.AddReel (reel1);
		reels.AddReel (reel2);
		reels.AddReel (reel3);

		List<List<Symbol>> window =
			ReelWindow.GetReelWindow (reels, new List<int> { 0, 0, 0 });

		Assert.AreEqual (3, window.Count);

		Assert.AreEqual (1, window[0].Count);
		Assert.AreEqual (1, window[1].Count);
		Assert.AreEqual (1, window[2].Count);

		Assert.AreEqual ("AA", window[0][0].Name);
		Assert.AreEqual ("BB", window[1][0].Name);
		Assert.AreEqual ("CC", window[2][0].Name);
	}

	[Test]
	public void ReelWindow_DefaultHeight()
	{
		reels = new ReelGroup ();

		ReelStrip reel1 = new ReelStrip ();
		reel1.AddSymbol (new Symbol (0, "AA"));
		reel1.AddSymbol (new Symbol (1, "BB"));
		reel1.AddSymbol (new Symbol (2, "CC"));
		reel1.AddSymbol (new Symbol (3, "DD"));
		reel1.AddSymbol (new Symbol (4, "EE"));

		reels.AddReel (reel1);

		List<List<Symbol>> window =
			ReelWindow.GetReelWindow (reels, new List<int> { 0 });

		Assert.AreEqual (3, window[0].Count);
	}

	[Test]
	public void ReelWindow_SpecifiedHeight()
	{
		reels = new ReelGroup ();

		ReelStrip reel1 = new ReelStrip ();
		reel1.AddSymbol (new Symbol (0, "AA"));
		reel1.AddSymbol (new Symbol (1, "BB"));
		reel1.AddSymbol (new Symbol (2, "CC"));
		reel1.AddSymbol (new Symbol (3, "DD"));
		reel1.AddSymbol (new Symbol (4, "EE"));

		reels.AddReel (reel1, 5);

		List<List<Symbol>> window =
			ReelWindow.GetReelWindow (reels, new List<int> { 0 });

		Assert.AreEqual (5, window[0].Count);
	}

	[Test]
	public void ReelWindow_CorrectSymbols()
	{
		reels = new ReelGroup ();

		ReelStrip reel1 = new ReelStrip ();
		reel1.AddSymbol (new Symbol (0, "AA"));
		reel1.AddSymbol (new Symbol (1, "BB"));
		reel1.AddSymbol (new Symbol (2, "CC"));
		reel1.AddSymbol (new Symbol (3, "DD"));
		reel1.AddSymbol (new Symbol (4, "EE"));

		reels.AddReel (reel1);

		List<List<Symbol>> window =
			ReelWindow.GetReelWindow (reels, new List<int> { 0 });

		ReelStrip expected = new ReelStrip ();
		expected.AddSymbol (new Symbol (0, "AA"));
		expected.AddSymbol (new Symbol (1, "BB"));
		expected.AddSymbol (new Symbol (2, "CC"));

		CollectionAssert.AreEqual (expected.Strip, window[0]);

		window = ReelWindow.GetReelWindow (reels, new List<int> { 1 });

		expected = new ReelStrip ();
		expected.AddSymbol (new Symbol (1, "BB"));
		expected.AddSymbol (new Symbol (2, "CC"));
		expected.AddSymbol (new Symbol (3, "DD"));

		CollectionAssert.AreEqual (expected.Strip, window[0]);

		window = ReelWindow.GetReelWindow (reels, new List<int> { 2 });

		expected = new ReelStrip ();
		expected.AddSymbol (new Symbol (2, "CC"));
		expected.AddSymbol (new Symbol (3, "DD"));
		expected.AddSymbol (new Symbol (4, "EE"));

		CollectionAssert.AreEqual (expected.Strip, window[0]);

		window = ReelWindow.GetReelWindow (reels, new List<int> { 3 });

		expected = new ReelStrip ();
		expected.AddSymbol (new Symbol (3, "DD"));
		expected.AddSymbol (new Symbol (4, "EE"));
		expected.AddSymbol (new Symbol (0, "AA"));

		CollectionAssert.AreEqual (expected.Strip, window[0]);

		window = ReelWindow.GetReelWindow (reels, new List<int> { 4 });

		expected = new ReelStrip ();
		expected.AddSymbol (new Symbol (4, "EE"));
		expected.AddSymbol (new Symbol (0, "AA"));
		expected.AddSymbol (new Symbol (1, "BB"));

		CollectionAssert.AreEqual (expected.Strip, window[0]);

		window = ReelWindow.GetReelWindow (reels, new List<int> { 5 });

		expected = new ReelStrip ();
		expected.AddSymbol (new Symbol (0, "AA"));
		expected.AddSymbol (new Symbol (1, "BB"));
		expected.AddSymbol (new Symbol (2, "CC"));

		CollectionAssert.AreEqual (expected.Strip, window[0]);
	}
}
