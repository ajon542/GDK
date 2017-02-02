using UnityEngine;
using System.Collections;

namespace GDK.Reels
{
	public class ReelSettings : MonoBehaviour
	{
		[SerializeField] private int visibleSymbols;
		[SerializeField] private float spinTime;
		[SerializeField] private float symbolSpacing;

		public int VisibleSymbols { get { return visibleSymbols; } }

		public float SpinTime { get { return spinTime; } }

		public float SymbolSpacing { get { return symbolSpacing; } }
	}
}
