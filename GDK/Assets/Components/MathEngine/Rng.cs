using System;

namespace GDK.MathEngine
{
	/// <summary>
	/// Warpper for System.Random, no need for anything else at this point.
	/// </summary>
	public class Rng : IRng
	{
        private Random random = new Random();

		public int GetRandomNumber ()
		{
			return random.Next ();
		}

		public int GetRandomNumber (int maximum)
		{
			return random.Next (maximum);
		}

		public int GetRandomNumber (int minimum, int maximum)
		{
			return random.Next (minimum, maximum);
		}
	}
}