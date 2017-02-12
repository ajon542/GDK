using UnityEngine;
using UnityEditor;
using NUnit.Framework;

using GDK.Utilities;

public class WIntegerTests
{
    [Test]
    public void WInteger_Value1()
    {
        WInteger w = new WInteger(0, 0, 100);
        Assert.AreEqual(0, w.Value);
    }

    [Test]
    public void WInteger_Value2()
    {
        WInteger w = new WInteger(99, 0, 100);
        Assert.AreEqual(99, w.Value);
    }

    [Test]
    public void WInteger_Value3()
    {
        WInteger w = new WInteger(100, 0, 100);
        Assert.AreEqual(0, w.Value);
    }

    [Test]
    public void WInteger_Value4()
    {
        WInteger w = new WInteger(150, 0, 100);
        Assert.AreEqual(50, w.Value);
    }

    [Test]
    public void WInteger_Value5()
    {
        WInteger w = new WInteger(101, 0, 100);
        Assert.AreEqual(1, w.Value);
    }

    [Test]
    public void WInteger_Value6()
    {
        WInteger w = new WInteger(-1, 0, 100);
        Assert.AreEqual(99, w.Value);
    }

    [Test]
    public void WInteger_Add1()
    {
        WInteger w = new WInteger(0, 0, 10);
        w.Add(10);
        Assert.AreEqual(0, w.Value);
    }

    [Test]
    public void WInteger_Add2()
    {
        WInteger w = new WInteger(0, 0, 10);
        w.Add(11);
        Assert.AreEqual(1, w.Value);
    }

    [Test]
    public void WInteger_Add3()
    {
        WInteger w = new WInteger(0, 0, 10);
        w.Add(21);
        Assert.AreEqual(1, w.Value);
    }

    [Test]
    public void WInteger_Add4()
    {
        WInteger w = new WInteger(0, 0, 10);
        w.Add(10001);
        Assert.AreEqual(1, w.Value);
    }
}
