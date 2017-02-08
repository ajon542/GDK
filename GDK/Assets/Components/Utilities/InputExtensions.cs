using UnityEngine;
using System.Collections;

namespace GDK.Utilities
{
	public static class InputExtensions
	{
		public static bool GetTouchDown ()
		{
			return Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began;
		}
	}
}
