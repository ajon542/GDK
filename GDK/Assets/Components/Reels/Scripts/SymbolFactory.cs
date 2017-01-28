using UnityEngine;
using System;
using System.Collections.Generic;

namespace GDK.Reels
{
	/// <summary>
	/// Implementation of a symbol factory that maps the symbol name to it's prefab.
	/// </summary>
	public class SymbolFactory : MonoBehaviour, ISymbolFactory
	{
		[Serializable]
		private class SymbolMap
		{
			public string name = string.Empty;
			public GameObject prefab = null;
		}

		[SerializeField]
		private List<SymbolMap> symbolPrefabs;

		private Dictionary<string, GameObject> symbolMap;

		private void Awake ()
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

		public GameObject CreateSymbol (string symbolName)
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