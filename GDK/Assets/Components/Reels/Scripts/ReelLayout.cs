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

		private List<GameObject> symbolContainers = new List<GameObject> ();
		private List<GameObject> symbolObjects = new List<GameObject> ();

		private AnimationCurve layoutCurve;
		private Vector3 symbolEnterPosition;
		private float symbolHeight;

		private Vector3 initialPosition;

		void Start ()
		{
			DOTween.Init (false, true, LogBehaviour.ErrorsOnly);

			initialPosition = gameObject.transform.position;

			// Create a set of center aligned symbol positions.
			layoutCurve = AnimationCurve.Linear (0, reelHeight, 1, -reelHeight);
			float topSymbolPos = 1.0f / (visibleSymbols + 1);
			symbolEnterPosition = new Vector3 (gameObject.transform.position.x, layoutCurve.Evaluate (topSymbolPos));
			symbolHeight = reelHeight - layoutCurve.Evaluate (topSymbolPos);

			for (int i = 1; i <= visibleSymbols; ++i)
			{
				// Create the symbol containers. These will define the layout of each symbol on the reel.
				float y = layoutCurve.Evaluate (i * topSymbolPos);
				var symbolContainer = new GameObject();
				symbolContainer.name = "SymbolContainer";
				symbolContainer.transform.position = new Vector3 (gameObject.transform.position.x, y, -1);
				symbolContainer.transform.parent = gameObject.transform;
				symbolContainers.Add (symbolContainer);

				// Attach the symbol as a child game object.
				var symbolObject = PoolManager.Obtain (symbolFactory.CreateSymbol ("AA"));
				symbolObject.transform.parent = symbolContainer.transform;
				symbolObject.transform.localPosition = Vector3.zero;
				symbolObjects.Add (symbolObject);
			}
		}

		private bool spinning;

		private void Update ()
		{
			if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
			{
				if (!spinning)
					SetTween ();

				spinning = !spinning;
			}
		}

		private void OnComplete ()
		{
			// Return the last symbol object to the pool.
			PoolManager.Return (symbolObjects[symbolObjects.Count - 1]);

			// Shuffle all symbols down.
			for (int i = symbolObjects.Count - 1; i > 0; --i)
			{
				symbolObjects [i] = symbolObjects[i - 1];
				symbolObjects [i].transform.parent = symbolContainers [i].transform;
				symbolObjects [i].transform.localPosition = Vector3.zero;
			}

			// Pick a random symbol for fun!
			List<ReelProperties> reelProps = paytable.ReelGroup.Reels;
			List<Symbol> symbols = reelProps [0].ReelStrip.Symbols;
			int random = rng.GetRandomNumber (symbols.Count);

			// Add the new symbol object.
			symbolObjects [0] = PoolManager.Obtain (symbolFactory.CreateSymbol (symbols[random].Name));
			symbolObjects [0].transform.parent = symbolContainers [0].transform;
			symbolObjects [0].transform.localPosition = Vector3.zero;

			// Reset the reel mover.
			gameObject.transform.position = initialPosition;

			if (spinning)
				SetTween ();
		}

		private void SetTween ()
		{
			gameObject.transform.DOMove (
				new Vector3 (initialPosition.x, initialPosition.y - symbolHeight, initialPosition.z), speed)
				.SetEase (Ease.Linear)
				.OnComplete (OnComplete);
		}
	}
}