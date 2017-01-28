using UnityEngine;
using System.Collections.Generic;

using GDK.Pool;

using GDK.MathEngine;
using Zenject;

namespace GDK.Reels
{
	public class ReelLayout : MonoBehaviour
	{
		[SerializeField]
		private int visibleSymbols;

		[Inject] Paytable paytable;
		[Inject] IRng rng;
		[Inject] ISymbolFactory symbolFactory;

		private List<GameObject> symbolObjects = new List<GameObject> ();
		private AnimationCurve layoutCurve = AnimationCurve.Linear (0, 10, 1, -10);
		private float topSymbolPosition;
		private float symbolEnterPosition;

		[SerializeField]
		private float speed = 0.003f;

		// NOTE: PROTOTYPE CODE ONLY
		// This code is written mainly to test some ideas out.
			// Reel spin curves
			// Reel spin stuff - pull random number, spin, splice, stop
			// Mapping the reel-model to the reel-view
			// Messing with the object pool
			// Messing with zenject

		void OnDrawGizmos ()
		{
			Gizmos.color = Color.green;

			// Get the position of the top symbol. This will never change.
			// All other symbol positions will be calculation from this value.
			int totalSymbols = visibleSymbols;

			symbolEnterPosition = 1.0f / (totalSymbols + 1);
			topSymbolPosition = symbolEnterPosition;

			for (int i = 1; i <= totalSymbols; ++i)
			{
				float y = layoutCurve.Evaluate (i * topSymbolPosition);

				Gizmos.DrawCube (new Vector3 (gameObject.transform.position.x, y, -1), Vector3.one);
			}
		}
			
		void Start ()
		{
			// Get the position of the top symbol. This will never change.
			// All other symbol positions will be calculation from this value.
			int totalSymbols = visibleSymbols;

			symbolEnterPosition = 1.0f / (totalSymbols + 1);
			topSymbolPosition = symbolEnterPosition;

			for (int i = 1; i <= totalSymbols; ++i)
			{
				float y = layoutCurve.Evaluate (i * topSymbolPosition);

				GameObject symbol = PoolManager.Obtain (symbolFactory.CreateSymbol("AA"));
				symbol.transform.position = new Vector3 (gameObject.transform.position.x, y, -1);
				symbolObjects.Add (symbol);
			}
		}

		void Update ()
		{
			// Calculate the position of all symbols based on the position of the top symbol.
			float symbolPosition = topSymbolPosition;

			foreach (GameObject symbol in symbolObjects)
			{
				float y = layoutCurve.Evaluate (symbolPosition);
				Vector3 currentPos = symbol.transform.position;

				// TODO: Making the assumption the curve just represents y-coord. Though, this
				// can simply be a single implementation. Other implementations can provide the
				// specifics of any sort of movement.
				currentPos.y = y;
				symbol.transform.position = currentPos;
				symbolPosition += symbolEnterPosition;
			}

			// TODO: Deal with the speed of a spin.
			// TODO: Deal with moving backwards / forwards (possibly based on user interaction).
			topSymbolPosition += speed;

			// TODO: Probably want to use a linked list to be able to remove from head and tail efficiently.
			// TODO: Reduce the curve Evaluate method calls
			if (layoutCurve.Evaluate (symbolPosition - symbolEnterPosition) <= -10.0f)
			{
				// Pull random symbol for the next one to be shown. Obviously not the normal thing to do!
				List<ReelProperties> reelProps = paytable.ReelGroup.Reels;
				List<Symbol> symbols = reelProps [0].ReelStrip.Symbols;
				int random = rng.GetRandomNumber (symbols.Count);

				// Add a new symbol to the start of the list.
				topSymbolPosition -= symbolEnterPosition;

				GameObject symbol = PoolManager.Obtain (symbolFactory.CreateSymbol(symbols[random].Name));
				symbol.transform.position = new Vector3 (gameObject.transform.position.x, layoutCurve.Evaluate (topSymbolPosition));
				symbolObjects.Insert (0, symbol);

				// Remove the last symbol.
				PoolManager.Return (symbolObjects [symbolObjects.Count - 1]);
				symbolObjects.RemoveAt (symbolObjects.Count - 1);
			}
		}
	}
}