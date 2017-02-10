using UnityEngine;
using System.Collections.Generic;

namespace GDK.Reels
{
    public interface IReelLayout
    {
        void Layout(GameObject root, out List<GameObject> gameObjects);
    }
}