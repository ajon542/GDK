using System;
using System.Collections.Generic;

namespace GDK.MathEngine
{
	[Serializable]
	public class PickItem
	{
		public string Name { get; set; }

		public int Value { get; set; }

		public PaytableTrigger Trigger { get; set; }
	}

	[Serializable]
	public class PickTable
	{
		public string Name { get; set; }

		public List<PickItem> PickItemList { get; set; }

		public PickTable(string name)
		{
			Name = name;
			PickItemList = new List<PickItem> ();
		}
	}

	[Serializable]
	public class PickTableGroup
	{
		public Dictionary<string, PickTable> PickTable { get; set; }

		public PickTableGroup ()
		{
			PickTable = new Dictionary<string, PickTable> ();
		}
	}
}
