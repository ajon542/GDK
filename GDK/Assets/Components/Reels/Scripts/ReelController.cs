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
						reelDisplays [0].Spin(new SymbolStream()),
						reelDisplays [1].Spin(new SymbolStream()),
						reelDisplays [2].Spin(new SymbolStream()),
						reelDisplays [3].Spin(new SymbolStream())
					}
				).Done(ReelSpinComplete);
			}
		}

		private void ReelSpinComplete()
		{
			Debug.Log ("Reel Spin Complete");
		}
	}
}
