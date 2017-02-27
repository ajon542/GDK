using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace GDK.MathEngine
{
	public class JsonPaytableBuilder : PaytableBuilder
	{
		private Paytable paytable;

		public JsonPaytableBuilder()
		{
			string result = File.ReadAllText ("Assets/Components/MathEngine/UnitTests/Data/PaylineEvaluatorTest-Paytable.txt");
			paytable = JsonConvert.DeserializeObject<Paytable> (result);
		}

		public override ReelGroup BuildReelGroup ()
		{
			return paytable.BaseGameReelGroup;
		}

		public override PaylineGroup BuildPaylineGroup ()
		{
			return paytable.PaylineGroup;
		}

		public override PayComboGroup BuildPayComboGroup ()
		{
			return paytable.PayComboGroup;
		}

		public override PayComboGroup BuildScatterComboGroup ()
		{
			return paytable.ScatterComboGroup;
		}

		public override PaytableTriggerGroup BuildPaytableTriggerGroup ()
		{
			return paytable.PaytableTriggerGroup;
		}

		public override PickTableGroup BuildPickTableGroup ()
		{
			return paytable.PickTableGroup;
		}
	}
}
