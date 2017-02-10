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

		private List<string> symbolStream;

		private int currentSymbolIndexInStream;

		private Promise spinCompletePromise;

		public Promise Spin (List<string> symbolStream)
		{
			spinCompletePromise = new Promise ();

			if (symbolStream == null || symbolStream.Count == 0)
			{
				spinCompletePromise.Reject(new Exception("attempting to spin reel with no symbols in the stream"));
				return spinCompletePromise;
			}

			currentSymbolIndexInStream = 0;
			this.symbolStream = symbolStream;
			SetTween ();

			return spinCompletePromise;
		}

		private void Start ()
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
				var symbolObject = PoolManager.Obtain (symbolFactory.CreateSymbol ("AA"));
                symbolContainers[i].AddChild(symbolObject);
				symbolObjects.Add (symbolObject);
			}
		}

		private void SetTween ()
		{
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

			// Add the new symbol object.
			symbolObjects [0] = PoolManager.Obtain (
				symbolFactory.CreateSymbol (symbolStream [currentSymbolIndexInStream++]));
            symbolContainers[0].AddChild(symbolObjects[0]);

			// Reset the root object to its initial position.
            root.transform.localPosition = Vector3.zero;

			// Continue displaying the symbols in the stream.
			if (currentSymbolIndexInStream < symbolStream.Count)
				SetTween ();
			else
				spinCompletePromise.Resolve ();
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