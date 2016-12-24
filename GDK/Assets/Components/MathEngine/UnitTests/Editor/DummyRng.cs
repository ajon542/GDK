using UnityEngine;
using System.Collections.Generic;

namespace GDK.MathEngine
{
	// Unity Test Tools specifies it comes with NSubstitute but the namespace
	// cannot be found. Therefore I have to create this rubbish mock rng class. FFS Unity.
	public class DummyRng : IRng
	{
		private int counter;
		private List<int> randomNumbers;

		public DummyRng(List<int> randomNumbers)
		{
			this.randomNumbers = randomNumbers;
		}

		public int GetRandomNumber ()
		{
			return randomNumbers [counter++];
		}

		public int GetRandomNumber (int maximum)
		{
			return GetRandomNumber ();
		}

		public int GetRandomNumber (int minimum, int maximum)
		{
			return GetRandomNumber ();
		}
	}
}