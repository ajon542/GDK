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

		[SerializeField] private int visibleSymbols;
		[SerializeField] private float speed = 1f;
		[SerializeField] private float reelHeight = 10;

		private Queue<GameObject> symbolObjects = new Queue<GameObject> ();
		private AnimationCurve layoutCurve;
		private Vector3 symbolEnterPosition;
		private float symbolHeight;

		void Start ()
		{
			DOTween.Init (false, true, LogBehaviour.ErrorsOnly);

			layoutCurve = AnimationCurve.Linear (0, reelHeight, 1, -reelHeight);

			// TODO: This is a bit messy...
			float topSymbolPos = 1.0f / (visibleSymbols + 1);
			symbolEnterPosition = new Vector3 (gameObject.transform.position.x, layoutCurve.Evaluate (topSymbolPos));
			symbolHeight = reelHeight - layoutCurve.Evaluate (topSymbolPos);

			for (int i = visibleSymbols; i >= 1; --i)
			{
				float y = layoutCurve.Evaluate (i * topSymbolPos);

				GameObject symbol = PoolManager.Obtain (symbolFactory.CreateSymbol ("AA"));
				symbol.transform.position = new Vector3 (gameObject.transform.position.x, y, -1);
				symbolObjects.Enqueue (symbol);
			}
		}

		private bool spinning;

		private void Update ()
		{
			if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
			{
				if (!spinning)
					foreach (var item in symbolObjects)
						SetTween (item);

				spinning = !spinning;
			}
		}

		private void OnComplete (GameObject go)
		{
			// TODO: This seems like a bug.
			if (go.transform.position.y <= -reelHeight)
			{
				List<ReelProperties> reelProps = paytable.ReelGroup.Reels;
				List<Symbol> symbols = reelProps [0].ReelStrip.Symbols;
				int random = rng.GetRandomNumber (symbols.Count);

				GameObject symbol = PoolManager.Obtain (symbolFactory.CreateSymbol ("AA"));
				symbol.transform.position = symbolEnterPosition;
				symbolObjects.Enqueue (symbol);

				PoolManager.Return (symbolObjects.Dequeue ());

				go = symbol;
			}

			if (spinning)
				SetTween (go);
		}

		private void SetTween (GameObject go)
		{
			go.transform.DOMove (
				new Vector3 (0, -symbolHeight, 0), speed)
				.SetRelative ()
				.SetEase (Ease.Linear)
				.OnComplete (() => OnComplete (go));
		}
	}
}