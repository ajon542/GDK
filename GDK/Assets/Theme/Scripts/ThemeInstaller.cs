using UnityEngine;
using System.Collections;

using GDK.MathEngine;
using GDK.Reels;

using Zenject;

namespace Theme
{
	// This is a prototype of how to glue the reel strips defined in the paytable
	// to the presentation.
	public class ThemeInstaller : MonoInstaller<ThemeInstaller>
	{
		private Paytable paytable;

		[SerializeField] SymbolFactory symbolFactory;

		public override void InstallBindings ()
		{
			//InstallPaytable ();
			Container.Bind<ISymbolFactory> ().FromPrefab (symbolFactory).AsSingle();
			Container.Bind<IRng> ().FromInstance (new Rng());
		}

		private void InstallPaytable ()
		{
			Paytable paytable = new Paytable ();

			PaytableBuilder builder = new JsonPaytableBuilder ();
			paytable.ReelGroup = builder.BuildReelGroup ();
			paytable.PaylineGroup = builder.BuildPaylineGroup ();
			paytable.PayComboGroup = builder.BuildPayComboGroup ();
			paytable.ScatterComboGroup = builder.BuildScatterComboGroup ();
			paytable.PickTableGroup = builder.BuildPickTableGroup ();
			paytable.PaytableTriggerGroup = builder.BuildPaytableTriggerGroup ();

			Container.Bind<Paytable> ().FromInstance (paytable);
		}
	}
}
