using UnityEngine;
using System.Collections;
using StateMachine;

namespace Reels
{
	public class StateConfiguration : BaseMonoBehaviourState
	{
		private void Update()
		{
			if (IsActive == false)
			{
				return;
			}

			UnityEngine.Debug.Log ("StateConfiguration...");
		}
	}
}
