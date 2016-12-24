using UnityEngine;
using UnityEditor;
using NUnit.Framework;

using GDK.MathEngine;

public class EvaluationTests
{
	private IRng rng;
	private IEvaluator paytableEvaluator;
	private IPaytableGenerator paytableGenerator;

	[TestFixtureSetUp]
	public void Init ()
	{
		rng = new Rng ();
		paytableEvaluator = new Evaluator ();
		paytableGenerator = new DummyPaytableGenerator ();
	}

	[Test]
	public void EditorTest ()
	{
		SlotResults results = paytableEvaluator.Evaluate (paytableGenerator.Generate (), rng);
		Assert.IsNotNull (results);
	}
}
