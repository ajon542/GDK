using UnityEngine;
using System.Collections.Generic;

using GDK.Pool;
using GDK.MathEngine;
using DG.Tweening;
using Zenject;

namespace GDK.Reels
{
	public class ReelLayout : MonoBehaviour
	{
		[Inject] Paytable paytable;
		[Inject] IRng rng;
		[Inject] ISymbolFactory symbolFactory;

		private List<GameObject> symbolObjects = new List<GameObject> ();

		private float symbolEnterPosition;
		private AnimationCurve layoutCurve;

		[SerializeField]
		private int visibleSymbols;

		[SerializeField]
		private float speed = 1f;

		[SerializeField]
		private float reelHeight = 10;

		private GameObject symbolContainer;

		// NOTE: PROTOTYPE CODE ONLY
		// This code is written mainly to test some ideas out.
		// Reel spin curves
		// Reel spin stuff - pull random number, spin, splice, stop
		// Mapping the reel-model to the reel-view
		// Messing with the object pool
		// Messing with zenject

		void OnDrawGizmos ()
		{
			Gizmos.color = Color.grey;

			layoutCurve = AnimationCurve.Linear (0, reelHeight, 1, -reelHeight);

			symbolEnterPosition = 1.0f / (visibleSymbols + 1);

			for (int i = 1; i <= visibleSymbols; ++i)
			{
				float y = layoutCurve.Evaluate (i * symbolEnterPosition);

				Gizmos.DrawCube (new Vector3 (gameObject.transform.position.x, y, -1), Vector3.one);
			}
		}

		void Start ()
		{
			symbolContainer = new GameObject ();
			symbolContainer.transform.parent = gameObject.transform;
			symbolContainer.name = "SymbolContainer";

			DOTween.Init (false, true, LogBehaviour.ErrorsOnly);

			layoutCurve = AnimationCurve.Linear (0, reelHeight, 1, -reelHeight);

			symbolEnterPosition = 1.0f / (visibleSymbols + 1);

			float symbolHeight = reelHeight - layoutCurve.Evaluate (symbolEnterPosition);

			for (int i = 1; i <= visibleSymbols; ++i)
			{
				float y = layoutCurve.Evaluate (i * symbolEnterPosition);

				GameObject symbol = PoolManager.Obtain (symbolFactory.CreateSymbol ("AA"));
				symbol.transform.position = new Vector3 (gameObject.transform.position.x, y, -1);
				symbolObjects.Add (symbol);

				symbol.transform.DOMove (
					new Vector3 (0, -symbolHeight, 0), speed)
					.SetRelative ()
					.SetEase (Ease.Linear)
					.OnComplete(() => OnComplete (symbol));	
			}
		}

		private void OnComplete(GameObject go)
		{
			// DOTween makes things easier but there is still a tonne of logic in the OnComplete...
			// The problem is that we need to know when the symbol in the bottom row has finished
			// its tween. Once this happens, we are ready to add another symbol in the top row.

			float symbolHeight = reelHeight - layoutCurve.Evaluate (symbolEnterPosition);

			if (go.transform.position.y <= -reelHeight)
			{
				List<ReelProperties> reelProps = paytable.ReelGroup.Reels;
				List<Symbol> symbols = reelProps [0].ReelStrip.Symbols;
				int random = rng.GetRandomNumber (symbols.Count);

				GameObject symbol = PoolManager.Obtain (symbolFactory.CreateSymbol (symbols [random].Name));
				symbol.transform.position = new Vector3 (gameObject.transform.position.x, layoutCurve.Evaluate (symbolEnterPosition));
				symbolObjects.Insert (0, symbol);

				PoolManager.Return (symbolObjects [symbolObjects.Count - 1]);
				symbolObjects.RemoveAt (symbolObjects.Count - 1);

				go = symbol;
			}

			go.transform.DOMove (
				new Vector3 (0, -symbolHeight, 0), speed)
				.SetRelative ()
				.SetEase (Ease.Linear)
				.OnComplete(() => OnComplete (go));
		}
	}

	public class VisibleSymbolContainer
	{
	}
}