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
	public class ReelDisplay : MonoBehaviour, IReelDisplay
	{
		[Inject] ISymbolFactory symbolFactory;

		private List<GameObject> symbolContainers = new List<GameObject> ();
		private List<GameObject> symbolObjects = new List<GameObject> ();

		private ReelSettings settings;

		private Vector3 initialPosition;

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
			DOTween.Init (false, true, LogBehaviour.ErrorsOnly);

			settings = GetComponent<ReelSettings> ();
			initialPosition = gameObject.transform.position;

			// Create the symbol containers.
			for (int i = 0; i < settings.TotalSymbols; ++i)
			{
				var symbolContainer = new GameObject ();
				symbolContainer.name = "SymbolContainer";
				symbolContainer.transform.parent = gameObject.transform;
				symbolContainer.transform.localPosition = Vector3.zero;
				symbolContainers.Add (symbolContainer);
			}

			// Align the symbol containers.
			float reelHeight = (settings.TotalSymbols - 1) * settings.SymbolSpacing;
			AnimationCurveExtensions.AlignVerticalCenter (
				symbolContainers, 
				initialPosition.y + reelHeight / 2, 
				initialPosition.y - reelHeight / 2);

			// Attach the symbol as a child objects of the symbol containers.
			for (int i = 0; i < settings.TotalSymbols; ++i)
			{
				var symbolObject = PoolManager.Obtain (symbolFactory.CreateSymbol ("AA"));
				symbolObject.transform.parent = symbolContainers [i].transform;
				symbolObject.transform.localPosition = Vector3.zero;
				symbolObjects.Add (symbolObject);
			}
		}

		private void SetTween ()
		{
			gameObject.transform.DOMove (
				new Vector3 (
					initialPosition.x,
					initialPosition.y - settings.SymbolSpacing, 
					initialPosition.z), settings.SpinTime)
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
				symbolObjects [i].transform.parent = symbolContainers [i].transform;
				symbolObjects [i].transform.localPosition = Vector3.zero;
			}

			// Add the new symbol object.
			symbolObjects [0] = PoolManager.Obtain (
				symbolFactory.CreateSymbol (symbolStream [currentSymbolIndexInStream++]));
			symbolObjects [0].transform.parent = symbolContainers [0].transform;
			symbolObjects [0].transform.localPosition = Vector3.zero;

			// Reset the reel mover.
			gameObject.transform.position = initialPosition;

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
			symbolObjects [index].transform.parent = symbolContainers [index].transform;
			symbolObjects [index].transform.localPosition = Vector3.zero;
		}
	}
}