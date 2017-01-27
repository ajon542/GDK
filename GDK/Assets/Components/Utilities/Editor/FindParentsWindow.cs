using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class FindParentsWindow : EditorWindow
{
	private class Parents
	{
		public List<string> m_parents = new List<string> ();
	}

	private static Dictionary<string, Parents> m_guidMap;

	[MenuItem ("Window/Find Parents")]
	public static void ShowWindow ()
	{
		// guidMap will only be null before the first time the window is opened.
		if (m_guidMap == null)
			Sync ();

		// Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow (typeof(FindParentsWindow));
	}
		
	[MenuItem ("Assets/Find Parents")]
	private static void FindParents ()
	{
		ShowWindow ();

		string selectionGuid = Selection.assetGUIDs [0];

		Debug.Log ("<color=green>" + AssetDatabase.GUIDToAssetPath (selectionGuid) + " [" + selectionGuid + "]" + "</color>");

		if (m_guidMap.ContainsKey (selectionGuid) == false)
		{
			Debug.Log ("No parents");
			return;
		}
			
		var parents = m_guidMap [selectionGuid].m_parents;
		foreach (string parent in parents)
			Debug.Log (AssetDatabase.GUIDToAssetPath (parent) + " [" + parent + "]");
	}

	private void OnGUI()
	{
		for (int i = 0; i < 100; ++i)
		{
			GUI.Label (new Rect (), "hello");
		}
	}

	private void OnProjectChange ()
	{
		// Called when the window is open and assets are added, moved, deleted.
		Sync ();
	}

	private static void Sync ()
	{
		string[] parentGuids = AssetDatabase.FindAssets ("");

		// Map the guid to its parent guids.
		m_guidMap = new Dictionary<string, Parents> ();

		foreach (string guid in parentGuids)
		{
			// Obtain the dependent assets.
			string assetPath = AssetDatabase.GUIDToAssetPath (guid);

			string[] childrenAssets = AssetDatabase.GetDependencies (assetPath, false);

			foreach (var asset in childrenAssets)
			{
				// Obtain the child asset guid.
				string childGuid = AssetDatabase.AssetPathToGUID (asset);

				// Add the child guid to the map if it doesn't already exist.
				if (m_guidMap.ContainsKey (childGuid) == false)
					m_guidMap.Add (childGuid, new Parents ());

				// Add the parent guid to the child guid.
				m_guidMap [childGuid].m_parents.Add (guid);
			}
		}
	}
}
