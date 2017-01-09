using System;
using System.Collections.Generic;

namespace GDK.MathEngine.Evaluators
{
	public class PickEvaluator : IEvaluator
	{
		private string PickFeatureId { get; set; }

		public PickEvaluator (string pickFeatureId)
		{
			PickFeatureId = pickFeatureId;
		}

		public SlotResults Evaluate (Paytable paytable, IRng rng)
		{
			if (paytable.PickTableGroup.PickTable.ContainsKey (PickFeatureId) == false)
			{
				// Can't evaluate if it doesn't exist in paytable.
				return null;
			}

			PickTable pickTable = paytable.PickTableGroup.PickTable [PickFeatureId];

			// Make a copy of the list since we will be removing elements from it.
			List<PickItem> pickItems = new List<PickItem> (pickTable.PickItemList);

			int totalValue = 0;
			PickItem item;

			do
			{
				// Continue picking items until a trigger is found.
				int randomNumber = rng.GetRandomNumber (pickItems.Count);
				item = pickItems [randomNumber];
				totalValue = item.Value;
			} while (string.IsNullOrEmpty (item.Trigger.Name));
				
			// TODO: Need a way to add a trigger
			// TODO: Need a way to add a prize which isn't a paycombo/payline
			SlotResults slotResults = new SlotResults ();
			return slotResults;
		}
	}
}
