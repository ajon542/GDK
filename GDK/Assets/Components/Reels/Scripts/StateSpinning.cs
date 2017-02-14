using UnityEngine;
using System.Collections;
using GDK.StateMachine;

namespace GDK.Reels
{
	public class StateSpinning : BaseMonoBehaviourState
	{
		private void Update()
		{
			if (IsActive == false)
			{
				return;
			}

			UnityEngine.Debug.Log ("StateSpinning...");
		}
	}
}