using System.Collections.Generic;
using System.Text;

namespace GDK.MathEngine
{
	/// <summary>
	/// Represents a single payline coordinate in the reel window.
	/// </summary>
	public class PaylineCoord
	{
		/// <summary>
		/// Gets or sets the reel index.
		/// </summary>
		public int ReelIndex { get; set; }

		/// <summary>
		/// Gets or sets the offset into the reel window.
		/// </summary>
		public int Offset { get; set; }

		/// <summary>
		/// String representation of the PaylineCoord.
		/// </summary>
		/// <returns>A string representation of the PaylineCoord.</returns>
		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.AppendFormat ("ReelIndex: {0}, Offset: {1}", ReelIndex, Offset);
			return sb.ToString ();
		}
	}

	/// <summary>
	/// Represents a single payline. A payline consists of one or more
	/// <see cref="PaylineCoord"/>.
	/// </summary>
	public class Payline
	{
		/// <summary>
		/// Gets the list of payline coordinates.
		/// </summary>
		public List<PaylineCoord> PaylineCoords { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Payline"/> class.
		/// </summary>
		public Payline ()
		{
			PaylineCoords = new List<PaylineCoord> ();
		}

		/// <summary>
		/// Add a payline coordinate to the payline.
		/// </summary>
		/// <param name="coord">The payline coordinate.</param>
		public void AddPaylineCoord (PaylineCoord coord)
		{
			PaylineCoords.Add (coord);
		}
	}

	/// <summary>
	/// Represents a set, or group of paylines.
	/// </summary>
	public class PaylineGroup
	{
		/// <summary>
		/// Gets the paylines in the payline group.
		/// </summary>
		public List<Payline> Paylines { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PaylineGroup"/> class.
		/// </summary>
		public PaylineGroup ()
		{
			Paylines = new List<Payline> ();
		}

		/// <summary>
		/// Adds a payline to the payline group.
		/// </summary>
		/// <param name="payline">The payline to add.</param>
		public void AddPayline (Payline payline)
		{
			Paylines.Add (payline);
		}
	}
}
