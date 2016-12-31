using UnityEngine;
using System.Collections;
using StateMachine;

namespace Reels
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