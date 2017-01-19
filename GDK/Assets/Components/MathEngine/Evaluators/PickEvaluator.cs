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

			PickItem item;
			SlotResults results = new SlotResults ();

			do
			{
				// Continue picking items until a trigger is found.
				int randomNumber = rng.GetRandomNumber (pickItems.Count);
				item = pickItems [randomNumber];
				pickItems.RemoveAt(randomNumber);

				SlotResult slotResult = new SlotResult { 
					PickResult = new PickResult { 
						Name = item.Name, 
						Value = item.Value, 
						Trigger = item.Trigger.Name 
					}
				};

				results.Results.Add (slotResult);

			} while (string.IsNullOrEmpty (item.Trigger.Name));
				
			return results;
		}
	}
}
