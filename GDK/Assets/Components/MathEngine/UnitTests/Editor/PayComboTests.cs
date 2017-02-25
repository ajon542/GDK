using UnityEngine;
using UnityEditor;
using NUnit.Framework;

using System.Collections.Generic;
using GDK.MathEngine;

public class PayComboTests
{
	private List<Symbol> symbolsInPayline;
	private PayCombo combo;

	[TestFixtureSetUp]
	public void Init ()
	{
		symbolsInPayline = new List<Symbol> {
			new Symbol(0, "AA"),
			new Symbol(0, "AA"),
			new Symbol(0, "AA"),
		};

		combo = new PayCombo (symbolsInPayline, 1000);
	}

	[Test]
	public void PayCombo_Match1 ()
	{			
		Assert.AreEqual (1000, combo.PayAmount);

		List<Symbol> symbolsToMatch = new List<Symbol> {
			new Symbol(0, "AA"),
			new Symbol(0, "AA"),
			new Symbol(0, "AA"),
		};

		Assert.IsTrue (combo.IsMatch(new SymbolComparer(), symbolsToMatch));
	}

	[Test]
	public void PayCombo_Match2 ()
	{			
		List<Symbol> symbolsToMatch = new List<Symbol> {
			new Symbol(0, "AA"),
			new Symbol(0, "AA"),
			new Symbol(0, "AA"),
			new Symbol(0, "AA"),
		};

        Assert.IsTrue(combo.IsMatch(new SymbolComparer(), symbolsToMatch));
	}

	[Test]
	public void PayCombo_Match3 ()
	{	
		List<Symbol> symbolsToMatch = new List<Symbol> {
			new Symbol(0, "AA"),
			new Symbol(0, "AA"),
		};

        Assert.IsFalse(combo.IsMatch(new SymbolComparer(), symbolsToMatch));
	}

	[Test]
	public void PayCombo_Match4 ()
	{	
		List<Symbol> symbolsToMatch = new List<Symbol> {
			new Symbol(0, "AA"),
		};

        Assert.IsFalse(combo.IsMatch(new SymbolComparer(), symbolsToMatch));
	}

	[Test]
	public void PayCombo_Match5 ()
	{	
		List<Symbol> symbolsToMatch = new List<Symbol> {
			new Symbol(1, "BB"),
		};

        Assert.IsFalse(combo.IsMatch(new SymbolComparer(), symbolsToMatch));
	}

	[Test]
	public void PayCombo_Match6 ()
	{	
		List<Symbol> symbolsToMatch = new List<Symbol> {
			new Symbol(0, "AA"),
			new Symbol(0, "AA"),
			new Symbol(0, "AA"),
			new Symbol(1, "BB"),
		};

        Assert.IsTrue(combo.IsMatch(new SymbolComparer(), symbolsToMatch));
	}

	[Test]
	public void PayCombo_Match7 ()
	{	
		List<Symbol> symbolsToMatch = new List<Symbol> {
			new Symbol(1, "BB"),
			new Symbol(0, "AA"),
			new Symbol(0, "AA"),
			new Symbol(0, "AA"),
		};

        Assert.IsFalse(combo.IsMatch(new SymbolComparer(), symbolsToMatch));
	}
}
