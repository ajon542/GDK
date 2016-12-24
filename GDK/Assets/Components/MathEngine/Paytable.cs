using System.Collections.Generic;

namespace GDK.MathEngine
{
	/// <summary>
	/// Represents a simple paytable for a slot game.
	/// </summary>
	public class Paytable
	{
		/// <summary>
		/// The reel group.
		/// </summary>
		public ReelGroup ReelGroup { get; set; }

		/// <summary>
		/// The paylines.
		/// </summary>
		public PaylineGroup PaylineGroup { get; set; }

		/// <summary>
		/// The pay combinations.
		/// </summary>
		public PayComboGroup PayComboGroup { get; set; }
	}
}
