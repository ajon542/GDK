using UnityEngine;
using System;
using System.Collections.Generic;

namespace GDK.Reels
{
	public class SymbolFactory : MonoBehaviour, ISymbolFactory
	{
		[Serializable]
		public class SymbolMap
		{
			public string name;
			public GameObject prefab;
		}

		[SerializeField]
		private List<SymbolMap> symbolPrefabs;

		private Dictionary<string, GameObject> symbolMap;

		private void Awake()
		{
			symbolMap = new Dictionary<string, GameObject> ();
			foreach (var symbol in symbolPrefabs)
			{
				if (symbolMap.ContainsKey (symbol.name))
				{
					Debug.LogError ("symbol map already contains the key [" + symbol.name + "]");
				} else
				{
					symbolMap.Add (symbol.name, symbol.prefab);
				}
			}
		}

		public GameObject CreateSymbol(string symbolName)
		{
			if (symbolMap.ContainsKey (symbolName) == false)
			{
				Debug.LogError ("cannot create symbol [" + symbolName + "]");
				return null;
			}
			return symbolMap [symbolName];
		}
	}
}