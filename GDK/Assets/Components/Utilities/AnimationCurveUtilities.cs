using UnityEngine;
using System.Collections.Generic;

namespace GDK.Utilities
{
	public static class AnimationCurveExtensions
	{
		/// <summary>
		/// Align the game objects along the y-axis evenly between start and end values.
		/// </summary>
		/// <param name="gameObjects">The game objects to align.</param>
		/// <param name="valueStart">The start value (inclusive).</param>
		/// <param name="valueEnd">The end value (inclusive).</param>
		public static void AlignVerticalCenter (List<GameObject> gameObjects, float valueStart, float valueEnd)
		{
			AnimationCurve layoutCurve = AnimationCurve.Linear (0, valueStart, 1, valueEnd);
			float firstPos = 1.0f / (gameObjects.Count - 1);

			for (int i = 0; i < gameObjects.Count; ++i)
			{
				float y = layoutCurve.Evaluate (i * firstPos);
				UpdatePosition (gameObjects [i].transform, 0, y, 0);
			}
		}

		private static void UpdatePosition (Transform t, float x, float y, float z)
		{
			t.position = new Vector3 (t.position.x + x, t.position.y + y, t.position.z + z);
		}
	}
}