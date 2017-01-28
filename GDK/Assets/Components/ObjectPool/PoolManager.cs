using UnityEngine;
using System;
using System.Collections.Generic;

namespace GDK.Pool
{
	public class PoolManager : Singleton<PoolManager>
	{
		private Dictionary<GameObject, Pool> prefabLookup = new Dictionary<GameObject, Pool> ();
		private Dictionary<GameObject, Pool> instanceLookup = new Dictionary<GameObject, Pool> ();

		public static GameObject Obtain (GameObject prefab)
		{
			return Instance.Internal_Obtain (prefab);
		}

		public static void Return (GameObject go)
		{
			Instance.Internal_Return (go);
		}

		private GameObject Internal_Obtain (GameObject prefab)
		{
			if (prefabLookup.ContainsKey (prefab) == false)
			{
				prefabLookup.Add (prefab, gameObject.AddComponent<Pool> ());
				prefabLookup [prefab].Init (prefab);
			}

			Pool pool = prefabLookup [prefab];
			GameObject clone = pool.Obtain ();
			instanceLookup.Add (clone, pool);
			clone.SetActive (true);
			return clone;
		}

		private void Internal_Return (GameObject go)
		{
			if (instanceLookup.ContainsKey (go) == false)
			{
				throw new Exception ("game object did not originate from this pool manager");
			}

			instanceLookup [go].Return (go);
			go.SetActive (false);
			instanceLookup.Remove (go);
		}
	}
}