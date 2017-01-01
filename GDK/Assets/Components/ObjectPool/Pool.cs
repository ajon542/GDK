using UnityEngine;
using System;
using System.Collections.Generic;

namespace ObjectPool
{
	/// <summary>
	/// Quick and dirty implementation of a game object pool.
	/// Would be nice to have the pool displayed in an editor window at runtime.
	/// </summary>
	public class Pool : MonoBehaviour
	{
		public int Capacity { get; private set; }

		private Queue<GameObject> pool = new Queue<GameObject> ();

		private Dictionary<int, bool> objectStatus = new Dictionary<int, bool> ();

		public Pool (GameObject gameObject, int capacity)
		{
			AddNewObjectsToPool (capacity);
		}

		public GameObject Obtain ()
		{
			if (pool.Count == 0)
			{
				AddNewObjectToPool ();
			}

			return ObtainObjectFromPool ();
		}

		public void Return (GameObject gameObject)
		{
			ReturnObjectToPool (gameObject);
		}

		public void Destroy ()
		{
			if (pool.Count != Capacity)
			{
				Debug.LogWarning (string.Format ("destroying a pool that has {0} unreturned items", Capacity - pool.Count));
			}

			while (pool.Count > 0)
			{
				DestroyImmediate (ObtainObjectFromPool ());
			}
			objectStatus.Clear ();
		}

		private void AddNewObjectsToPool (int count)
		{
			for (int i = 0; i < count; ++i)
			{
				AddNewObjectToPool ();
			}
		}

		private void AddNewObjectToPool ()
		{
			GameObject go = Instantiate (gameObject) as GameObject;
			pool.Enqueue (go);
			objectStatus.Add (go.GetInstanceID (), true);
			Capacity++;
		}

		private GameObject ObtainObjectFromPool ()
		{
			GameObject go = pool.Dequeue ();
			objectStatus [go.GetInstanceID ()] = false;
			return go;
		}

		private void ReturnObjectToPool (GameObject go)
		{
			int instanceId = go.GetInstanceID ();
			if (objectStatus.ContainsKey (instanceId) == false)
			{
				throw new Exception ("game object did not originate from this pool");
			}

			objectStatus [instanceId] = true;
			pool.Enqueue (go);
		}
	}
}
