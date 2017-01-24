using UnityEngine;
using System;
using System.Collections.Generic;

namespace GDK.Pool
{
	// An object pool is an essential part of the game. We should really make
	// an editor window for runtime object analysis. We should be able to look at this
	// window and see all objects (not)present in the pool and possibly who obtained them.
	// Being able to see who obtained them might be useful especially if a user can obtain
	// objects from a pool they did not create.
	// With the runtime analysis, it may turn out some pools are under/over-utilized
	// and we can tune each accordingly.

	/// <summary>
	/// Quick and dirty implementation of a game object pool.
	/// Would be nice to have the pool displayed in an editor window at runtime.
	/// </summary>
	public class Pool : MonoBehaviour
	{
		private GameObject pooledObject;

		public int Capacity { get; private set; }

		private Queue<GameObject> pool = new Queue<GameObject> ();

		private Dictionary<int, bool> objectStatus = new Dictionary<int, bool> ();

		public void Init(GameObject pooledObject)
		{
			this.pooledObject = pooledObject;
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

		private void AddNewObjectToPool ()
		{
			GameObject go = Instantiate (pooledObject) as GameObject;
			go.transform.parent = gameObject.transform;
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
