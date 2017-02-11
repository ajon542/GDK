using UnityEngine;
using System;
using System.Collections.Generic;

using GDK.Pool;
using GDK.MathEngine;
using GDK.Utilities;
using DG.Tweening;
using RSG;
using Zenject;

namespace GDK.Reels
{
	[RequireComponent (typeof(ReelSettings))]
    [RequireComponent(typeof(ReelLayout))]
	public class ReelDisplay : MonoBehaviour, IReelDisplay
	{
		[Inject] ISymbolFactory symbolFactory;

        private GameObject root;
        private List<GameObject> symbolContainers;
		private List<GameObject> symbolObjects = new List<GameObject> ();

		private ReelSettings settings;

		private ISymbolStream symbolStream;

		private Promise spinCompletePromise;

        private void Start()
        {
            // Create the root reel object.
            root = new GameObject();
            root.name = "Root";
            gameObject.AddChild(root);

            // Get the reel settings and align all the symbol container game objects.
            settings = GetComponent<ReelSettings>();
            GetComponent<ReelLayout>().Layout(root, out symbolContainers);

            // Attach the symbol as a child objects of the symbol containers.
            for (int i = 0; i < settings.TotalSymbols; ++i)
            {
                var symbolObject = PoolManager.Obtain(symbolFactory.CreateSymbol("AA"));
                symbolContainers[i].AddChild(symbolObject);
                symbolObjects.Add(symbolObject);
            }
        }

        public Promise Spin(ISymbolStream symbolStream)
		{
			spinCompletePromise = new Promise ();
			this.symbolStream = symbolStream;
			SpinToNextSymbol ();

			return spinCompletePromise;
		}

		private void SpinToNextSymbol ()
		{
            // If there are no more symbols in the stream, we are done spinning.
            if (string.IsNullOrEmpty(symbolStream.Peek()))
            {
                spinCompletePromise.Resolve();
                return;
            }

            // Move the reel down per the spin settings.
			root.transform.DOMove (
				new Vector3 (
					gameObject.transform.position.x,
                    gameObject.transform.position.y - settings.SymbolSpacing,
                    gameObject.transform.position.z), settings.SpinTime)
				.SetEase (Ease.Linear)
				.OnComplete (OnComplete);
		}

		private void OnComplete ()
		{
			// Return the last symbol object to the pool.
			PoolManager.Return (symbolObjects [symbolObjects.Count - 1]);

			// Shuffle all symbols down.
            for (int i = symbolObjects.Count - 1; i > 0; --i)
            {
                symbolObjects [i] = symbolObjects [i - 1];
                symbolContainers[i].AddChild(symbolObjects[i]);
            }

            // Add the next symbol to the reel.
            string symbol = symbolStream.NextSymbol();
            symbolObjects[0] = PoolManager.Obtain(symbolFactory.CreateSymbol(symbol));
            symbolContainers[0].AddChild(symbolObjects[0]);

			// Reset the root object to its initial position.
            root.transform.localPosition = Vector3.zero;

			// Continue displaying the symbols in the stream.
		    SpinToNextSymbol ();
		}

		public void ReplaceSymbol (int index, string symbol)
		{
			if (index < 0 || index >= settings.TotalSymbols)
			{
				Debug.LogError ("cannot replace symbol, index is out of range");
				return;
			}

			PoolManager.Return (symbolObjects [index]);
			symbolObjects [index] = PoolManager.Obtain (symbolFactory.CreateSymbol (symbol));
            symbolContainers[index].AddChild(symbolObjects[index]);
		}
	}
}