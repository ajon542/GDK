using System;
using System.Collections.Generic;

namespace GDK.MathEngine
{
	public class Result
	{
	}

	/// <summary>
	/// Container for <see cref="PayCombo"/> and <see cref="Payline"/> which
	/// represents a single result for a slot style game.
	/// </summary>
	public class SlotResult : Result
	{
		public SlotResult (PayCombo payCombo, Payline payline)
		{
			PayCombo = payCombo;
			Payline = payline;
		}

		public PayCombo PayCombo { get; private set; }

		public Payline Payline { get; private set; }
	}

	public class PickResult : Result
	{
		public int Value { get; set; }
		public PaytableTrigger Trigger { get; set; }
	}

	public class SlotResults
	{
		public SlotResults ()
		{
			Results = new List<Result> ();
		}

		public List<Result> Results { get; set; }
	}
}
