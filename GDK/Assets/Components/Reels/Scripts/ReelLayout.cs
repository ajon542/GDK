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

		private Queue<GameObject> symbolObjects = new Queue<GameObject> ();

		private float symbolEnterPosition;
		private AnimationCurve layoutCurve;

		[SerializeField]
		private int visibleSymbols;

		[SerializeField]
		private float speed = 1f;

		[SerializeField]
		private float reelHeight = 10;

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
			DOTween.Init (false, true, LogBehaviour.ErrorsOnly);

			layoutCurve = AnimationCurve.Linear (0, reelHeight, 1, -reelHeight);

			symbolEnterPosition = 1.0f / (visibleSymbols + 1);

			for (int i = visibleSymbols; i >= 1; --i)
			{
				float y = layoutCurve.Evaluate (i * symbolEnterPosition);

				GameObject symbol = PoolManager.Obtain (symbolFactory.CreateSymbol ("AA"));
				symbol.transform.position = new Vector3 (gameObject.transform.position.x, y, -1);
				symbolObjects.Enqueue (symbol);

				SetTween (symbol);	
			}
		}

		private void OnComplete()
		{
			List<ReelProperties> reelProps = paytable.ReelGroup.Reels;
			List<Symbol> symbols = reelProps [0].ReelStrip.Symbols;
			int random = rng.GetRandomNumber (symbols.Count);

			GameObject symbol = PoolManager.Obtain (symbolFactory.CreateSymbol (symbols [random].Name));
			symbol.transform.position = new Vector3 (gameObject.transform.position.x, layoutCurve.Evaluate (symbolEnterPosition));
			symbolObjects.Enqueue (symbol);
			PoolManager.Return (symbolObjects.Dequeue ());

			SetTween (symbol);
		}

		private void SetTween(GameObject go)
		{
			go.transform.DOMove (
				new Vector3 (
					gameObject.transform.position.x,
					layoutCurve.Evaluate (1),
					gameObject.transform.position.z), speed)
				.SetSpeedBased()
				.SetEase (Ease.Linear)
				.OnComplete(OnComplete);	
		}
	}
}