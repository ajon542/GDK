using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class CreditsTests
{
	[Test]
	public void Credits_Initialization ()
	{
		Credits credits = 5;
		Assert.AreEqual (5, credits);
	}

	[Test]
	public void Credits_Addition ()
	{
		Credits credits = 5;
		credits += credits;
		Assert.AreEqual (10, credits);
	}

	[Test]
	public void Credits_Subtraction ()
	{
		Credits credits = 5;
		credits -= credits;
		Assert.AreEqual (0, credits);
	}
}
