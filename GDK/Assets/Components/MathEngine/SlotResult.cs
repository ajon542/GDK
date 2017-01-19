using System;
using System.Collections.Generic;

namespace GDK.MathEngine
{
	/// <summary>
	/// Container for <see cref="PayCombo"/> and <see cref="Payline"/> which
	/// represents a single result for a slot style game.
	/// </summary>
	public class SlotResult
	{
		public PayCombo PayCombo { get; set; }
		public Payline Payline { get; set; }
		public PickResult PickResult { get; set; }

		public SlotResult()
		{
			PayCombo = new PayCombo ();
			Payline = new Payline ();
			PickResult = new PickResult ();
		}
	}

	public class PickResult
	{
		public string Name { get; set; }
		public int Value { get; set; }
		public string Trigger { get; set; }
	}

	public class SlotResults
	{
		public SlotResults ()
		{
			Results = new List<SlotResult> ();
		}

		public List<SlotResult> Results { get; set; }
	}
}
