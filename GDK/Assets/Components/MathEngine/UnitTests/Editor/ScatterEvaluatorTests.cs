using System.Collections.Generic;
using System.IO;
using GDK.MathEngine;
using GDK.MathEngine.Evaluators;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class ScatterEvaluatorTests
{
	private IRng rng;
	private IEvaluator scatterEvaluator;
	private Paytable paytable;

	[TestFixtureSetUp]
	public void Init ()
	{
		DummyPaytableGenerator generator = new DummyPaytableGenerator ();
		paytable = generator.Generate ();

		scatterEvaluator = new ScatterEvaluator ();

		//string json = JsonConvert.SerializeObject(paytable, Formatting.Indented);
		//File.WriteAllText ("/Users/andrew/development/logs/Paytable.txt", json);

		//string result = File.ReadAllText ("Assets/Components/MathEngine/UnitTests/Data/PaylineEvaluatorTest-Paytable.txt");
		//paytable = JsonConvert.DeserializeObject<Paytable> (result);
	}

	[Test]
	public void Evaluation_Initialization ()
	{
		rng = new DummyRng (new List<int> { 0, 0, 0 });
		SlotResults results = scatterEvaluator.Evaluate (paytable, rng);
		Assert.IsNotNull (results);
	}
}
