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
		// TODO: A SlotResult is going to be more than just a PayCombo and Payline.
		public SlotResult (PayCombo payCombo, Payline payline)
		{
			PayCombo = payCombo;
			Payline = payline;
		}

		public PayCombo PayCombo { get; private set; }

		public Payline Payline { get; private set; }
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
