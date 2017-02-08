using UnityEngine;
using System.Collections;

namespace GDK.Reels
{
	public class ReelSettings : MonoBehaviour
	{
		[SerializeField] private int topBuffer;
		[SerializeField] private int bottomBuffer;
		[SerializeField] private int visibleSymbols;
		[SerializeField] private float spinTime;
		[SerializeField] private float symbolSpacing;

		public int TopBuffer { get { return topBuffer; } }

		public int BottomBuffer { get { return bottomBuffer; } }

		public int VisibleSymbols { get { return visibleSymbols; } }

		public int TotalSymbols { get { return topBuffer + visibleSymbols + bottomBuffer; } }

		public float SpinTime { get { return spinTime; } }

		public float SymbolSpacing { get { return symbolSpacing; } }
	}
}
