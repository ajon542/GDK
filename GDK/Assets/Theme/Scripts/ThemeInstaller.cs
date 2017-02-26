using UnityEngine;
using System.Collections.Generic;

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

        [SerializeField]
        SymbolFactory symbolFactory;

        public override void InstallBindings()
        {
            InstallPaytable();
            Container.Bind<ISymbolFactory>().FromPrefab(symbolFactory).AsSingle();
            Container.Bind<IRng>().FromInstance(new Rng());
        }

        private void InstallPaytable()
        {
            Paytable paytable = new Paytable();

            paytable.ReelGroup = new ReelGroup();

            paytable.ReelGroup.AddReel(new ReelStrip
            {
                Symbols = new List<Symbol> { new Symbol(0, "AA"), new Symbol(1, "BB"), new Symbol(2, "CC"), new Symbol(3, "DD"), new Symbol(3, "EE"), new Symbol(4, "FF"), new Symbol(5, "GG"), new Symbol(6, "HH") }
            });

            paytable.ReelGroup.AddReel(new ReelStrip
            {
                Symbols = new List<Symbol> { new Symbol(0, "AA"), new Symbol(1, "BB"), new Symbol(2, "CC"), new Symbol(3, "DD"), new Symbol(3, "EE"), new Symbol(4, "FF"), new Symbol(5, "GG"), new Symbol(6, "HH") }
            });
            paytable.ReelGroup.AddReel(new ReelStrip
            {
                Symbols = new List<Symbol> { new Symbol(0, "AA"), new Symbol(1, "BB"), new Symbol(2, "CC"), new Symbol(3, "DD"), new Symbol(3, "EE"), new Symbol(4, "FF"), new Symbol(5, "GG"), new Symbol(6, "HH") }
            });
            paytable.ReelGroup.AddReel(new ReelStrip
            {
                Symbols = new List<Symbol> { new Symbol(0, "AA"), new Symbol(1, "BB"), new Symbol(2, "CC"), new Symbol(3, "DD"), new Symbol(3, "EE"), new Symbol(4, "FF"), new Symbol(5, "GG"), new Symbol(6, "HH") }
            });
            paytable.ReelGroup.AddReel(new ReelStrip
            {
                Symbols = new List<Symbol> { new Symbol(0, "AA"), new Symbol(1, "BB"), new Symbol(2, "CC"), new Symbol(3, "DD"), new Symbol(3, "EE"), new Symbol(4, "FF"), new Symbol(5, "GG"), new Symbol(6, "HH") }
            });

            Container.Bind<Paytable>().FromInstance(paytable);
        }
    }
}
