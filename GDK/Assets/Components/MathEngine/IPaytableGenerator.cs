using UnityEngine;
using System.Collections;

namespace GDK.MathEngine
{
	public interface IPaytableGenerator
	{
		Paytable Generate ();
	}
}