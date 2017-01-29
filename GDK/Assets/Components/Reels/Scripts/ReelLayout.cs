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
		private Vector3 symbolExitPosition;

		private void Start ()
		{
			DOTween.Init (false, true, LogBehaviour.ErrorsOnly);

			// To spin in reverse we just switch the sign on the reelHeight.
			layoutCurve = AnimationCurve.Linear (0, reelHeight, 1, -reelHeight);

			float symbolTopPos = 1.0f / (visibleSymbols + 1);

			symbolEnterPosition = new Vector3 (gameObject.transform.position.x, layoutCurve.Evaluate (symbolTopPos));
			symbolExitPosition = new Vector3 (gameObject.transform.position.x, layoutCurve.Evaluate (1));

			for (int i = visibleSymbols; i >= 1; --i)
			{
				float y = layoutCurve.Evaluate (i * symbolTopPos);

				GameObject symbol = PoolManager.Obtain (symbolFactory.CreateSymbol ("AA"));
				symbol.transform.position = new Vector3 (gameObject.transform.position.x, y, -1);
				symbolObjects.Enqueue (symbol);

				SetTween (symbol);	
			}
		}

		private bool spinning;

		private void Update ()
		{
			if (Input.GetKeyDown (KeyCode.Space))
			{
				spinning = !spinning;
			}
		}

		private void OnComplete ()
		{
			List<ReelProperties> reelProps = paytable.ReelGroup.Reels;
			List<Symbol> symbols = reelProps [0].ReelStrip.Symbols;
			int random = rng.GetRandomNumber (symbols.Count);

			GameObject symbol = PoolManager.Obtain (symbolFactory.CreateSymbol (symbols [random].Name));
			symbol.transform.position = symbolEnterPosition;
			symbolObjects.Enqueue (symbol);
			PoolManager.Return (symbolObjects.Dequeue ());

			SetTween (symbol);
		}

		private void SetTween (GameObject go)
		{
			go.transform.DOMove (symbolExitPosition, speed)
				.SetSpeedBased ()
				.SetEase (Ease.Linear)
				.OnComplete (OnComplete);	
		}
	}
}