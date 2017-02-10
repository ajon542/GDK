using UnityEngine;

namespace GDK.Utilities
{
    public static class GameObjectExtensions
    {
        public static void AddChild(this GameObject parent, GameObject child)
        {
            child.transform.parent = parent.transform;
            child.transform.localPosition = Vector3.zero;
        }
    }
}
