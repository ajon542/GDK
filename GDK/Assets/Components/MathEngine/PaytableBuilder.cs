using System;
using System.Collections.Generic;

namespace GDK.MathEngine
{
	public abstract class PaytableBuilder
	{
		public virtual ReelGroup BuildReelGroup ()
		{
			return new ReelGroup ();
		}

		public virtual PaylineGroup BuildPaylineGroup ()
		{
			return new PaylineGroup ();
		}

		public virtual PayComboGroup BuildPayComboGroup ()
		{
			return new PayComboGroup ();
		}

		public virtual PayComboGroup BuildScatterComboGroup ()
		{
			return new PayComboGroup ();
		}

		public virtual PaytableTriggerGroup BuildPaytableTriggerGroup ()
		{
			return new PaytableTriggerGroup ();
		}

		public virtual PickTableGroup BuildPickTableGroup ()
		{
			return new PickTableGroup ();
		}
	}
}