using UnityEngine;
using System.Collections;

namespace GDK.Reels
{
	public interface ISymbolFactory
	{
		GameObject CreateSymbol (string symbolName);
	}
}
