﻿using UnityEngine;
using System.Collections.Generic;
using GDK.StateMachine;
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
        [Inject] Paytable paytable;
        [SerializeField] List<ReelDisplay> reelDisplays;

        private List<SymbolStream> symbolStreams;

        private void Start()
        {
            if (reelDisplays.Count != paytable.BaseGameReelGroup.Reels.Count)
            {
                Debug.LogError(string.Format("The number of reel displays mistmatches the number of reels defined in the paytable"));
                return;
            }

            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
            symbolStreams = new List<SymbolStream>();

            for (int i = 0; i < reelDisplays.Count; ++i)
            {
                symbolStreams.Add(new SymbolStream());
                symbolStreams[i].Initialize(paytable.BaseGameReelGroup.Reels[i].ReelStrip.Symbols);
            }
        }

        int targetStop = 0;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || InputExtensions.GetTouchDown())
            {
                List<IPromise> reels = new List<IPromise>();
                for (int i = 0; i < reelDisplays.Count; ++i)
                {
                    symbolStreams[i].Reset();
                    reels.Add(reelDisplays[i].Spin(symbolStreams[i]));
                }

                Promise.All(reels).Done(ReelSpinComplete);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                foreach (var stream in symbolStreams)
                {
                    stream.Splice(targetStop);
                }
                targetStop++;
            }
        }

        private void ReelSpinComplete()
        {
            Debug.Log("Reel Spin Complete");
        }
    }
}
