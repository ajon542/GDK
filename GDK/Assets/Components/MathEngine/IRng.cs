using UnityEngine;
using System.Collections;

namespace GDK.MathEngine
{
	/// <summary>
	/// Interface for obtaining a random number.
	/// </summary>
	public interface IRng
	{
		/// <summary>
		/// Returns a non-negative random integer.
		/// </summary>
		/// <returns>The random number.</returns>
		int GetRandomNumber ();

		/// <summary>
		/// Returns a non-negative random integer that is less than the specified maximum.
		/// </summary>
		/// <returns>The random number.</returns>
		/// <param name="maximum">The exclusive upper bound of the random number to be generated. </param>
		int GetRandomNumber (int maximum);

		/// <summary>
		/// Get a random number in the range.
		/// </summary>
		/// <returns>The random number.</returns>
		/// <param name="min">The inclusive lower bound of the random number returned.</param>
		/// <param name="max">The exclusive upper bound of the random number returned.</param>
		int GetRandomNumber (int minimum, int maximum);
	}
}