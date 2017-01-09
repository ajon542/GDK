using UnityEngine;
using System.Collections.Generic;

namespace GDK.MathEngine
{
	/// <summary>
	/// A paytable trigger is an item in the paytable that can be triggered by a particular event.
	/// For example, 3 x scatter symbols can trigger the free spins, there "free spins" will be a
	/// paytable trigger.
	/// </summary>
	public class PaytableTrigger
	{
		public string Name { get; set; }
		public string Evaluator { get; set; }
	}

	public class PaytableTriggerGroup
	{
		public List<PaytableTrigger> PaytableTriggerList { get; set; }

		public PaytableTriggerGroup()
		{
			PaytableTriggerList = new List<PaytableTrigger> ();
		}
	}
}
