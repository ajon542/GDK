using System;
using System.IO;
using System.Xml.Serialization;

namespace GDK.MathEngine
{
	/// <summary>
	/// Class to (de)serialize the paytable data.
	/// </summary>
	/// <remarks>
	/// A paytable generator would normally be part of the math tools.
	/// The math would be implemented in spreadsheet or using a GUI.
	/// The tool would generate an XML/JSON file describing the math.
	/// The GDK would read this file in and create the appropriate structures.
	/// </remarks>
	public class PaytableGenerator
	{
		/// <summary>
		/// Create the paytable.
		/// </summary>
		private Paytable paytable = new Paytable ();

		/// <summary>
		/// Initializes a new instance of the <see cref="PaytableGenerator"/> class.
		/// </summary>
		public PaytableGenerator ()
		{
			// Construct a dummy paytable using hardcoded data.
			paytable.ConstructDummyPaytable ();
		}

		/// <summary>
		/// Serialize the paytable to a byte array.
		/// </summary>
		/// <returns>The byte array containing the xml serialized data.</returns>
		public byte[] Serialize ()
		{
			XmlSerializer x = new XmlSerializer (paytable.GetType ());
			using (var ms = new MemoryStream ())
			{
				x.Serialize (ms, paytable);
				return ms.ToArray ();
			}
		}

		/// <summary>
		/// Deserialize the data into a paytable object.
		/// </summary>
		/// <param name="data">The data.</param>
		public void Deserialize (byte[] data)
		{
			XmlSerializer x = new XmlSerializer (paytable.GetType ());
			using (var ms = new MemoryStream (data))
			{
				paytable = x.Deserialize (ms) as Paytable;
			}

			x.Serialize (Console.Out, paytable);
			Console.WriteLine ();
		}
	}
}
