using UnityEngine;
using System.Collections;

namespace GDK.Reels
{
    public interface ISymbolStream 
    {
        string NextSymbol();
        string Peek();
    }
}