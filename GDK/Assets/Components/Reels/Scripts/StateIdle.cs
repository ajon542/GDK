using UnityEngine;
using System.Collections;
using StateMachine;

namespace GDK.Reels
{
	public class StateIdle : BaseMonoBehaviourState
	{
		private void Update()
		{
			if (IsActive == false)
			{
				return;
			}
		}
	}
}