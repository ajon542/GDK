using UnityEngine;
using System.Collections.Generic;
using GDK.Pool;
using GDK.Utilities;

namespace GDK.Reels
{
    [RequireComponent(typeof(ReelSettings))]
    public class ReelLayout : MonoBehaviour, IReelLayout
    {
        public void Layout(GameObject root, out List<GameObject> symbolContainers)
        {
            symbolContainers = new List<GameObject>();
            ReelSettings settings = GetComponent<ReelSettings>();
            Vector3 initialPosition = root.transform.position;

            // Create the symbol containers.
            for (int i = 0; i < settings.TotalSymbols; ++i)
            {
                var symbolContainer = new GameObject();
                symbolContainer.name = "SymbolContainer";
                symbolContainer.transform.parent = root.transform;
                symbolContainer.transform.localPosition = Vector3.zero;
                symbolContainers.Add(symbolContainer);
            }

            // Align the symbol containers.
            float reelHeight = (settings.TotalSymbols - 1) * settings.SymbolSpacing;
            AnimationCurveExtensions.AlignVerticalCenter(
                symbolContainers,
                initialPosition.y + reelHeight / 2,
                initialPosition.y - reelHeight / 2);
        }
    }
}