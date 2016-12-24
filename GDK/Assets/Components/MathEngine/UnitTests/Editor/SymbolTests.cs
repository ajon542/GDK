using UnityEngine;
using UnityEditor;
using NUnit.Framework;

using GDK.MathEngine;

public class SymbolTests
{
	[Test]
	public void Symbol_Initialization ()
	{
		Symbol symbol = new Symbol (12, "Ace");

		Assert.AreEqual (12, symbol.Id);
		Assert.AreEqual ("Ace", symbol.Name);
	}

	[Test]
	public void Symbol_CopyConstruction ()
	{
		Symbol symbol = new Symbol (12, "Ace");
		Symbol copy = new Symbol (symbol);

		Assert.AreEqual (12, copy.Id);
		Assert.AreEqual ("Ace", copy.Name);
	}

	[Test]
	public void Symbol_Equality ()
	{
		Symbol symbol1 = new Symbol (12, "Ace");
		Symbol symbol2 = new Symbol (12, "Ace");

		Assert.AreEqual (symbol1, symbol2);
	}
}
