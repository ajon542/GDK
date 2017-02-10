using UnityEngine;
using System.Collections.Generic;
using StateMachine;
using GDK.MathEngine;
using GDK.Utilities;
using DG.Tweening;
using RSG;
using Zenject;

namespace GDK.Reels
{
	public class ReelController : MonoBehaviour
	{
		[Inject] IRng rng;
		//[Inject] Paytable paytable;

		[SerializeField] List<ReelDisplay> reelDisplays;

		//private List<int> symbolsToSpin;

		private void Start()
		{
			//symbolsToSpin = new List<int> { 22, 26, 30, 34 };

            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space) || InputExtensions.GetTouchDown())
			{
				Promise.All(
					new Promise[]
					{                   
						reelDisplays [0].Spin(new List<string> { "AA", "BB", "CC", "DD" }),
						reelDisplays [1].Spin(new List<string> { "AA", "BB", "CC", "DD" }),
						reelDisplays [2].Spin(new List<string> { "AA", "BB", "CC", "DD" }),
						reelDisplays [3].Spin(new List<string> { "AA", "BB", "CC", "DD" })
					}
				).Done(ReelSpinComplete);



				/*List<ReelProperties> reelProps = paytable.ReelGroup.Reels;
				List<Symbol> symbols = reelProps [0].ReelStrip.Symbols;

				for (int reel = 0; reel < reelDisplays.Count; ++reel)
				{
					List<string> symbolStream = new List<string> ();

					for (int i = 0; i < symbolsToSpin[reel]; ++i)
					{
						int random = rng.GetRandomNumber (symbols.Count);
						symbolStream.Add(symbols[random].Name);
					}

					reelDisplays [reel].Spin (symbolStream);
				}*/
			}
		}

		private void ReelSpinComplete()
		{
			Debug.Log ("Reel Spin Complete");
		}
	}
}
