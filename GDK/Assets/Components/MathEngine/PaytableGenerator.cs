using System;
using System.IO;
using System.Xml.Serialization;

namespace GDK.MathEngine
{
	public class PaytableGenerator : IPaytableGenerator
	{
		private string filename;
		private Paytable paytable;

		public PaytableGenerator (string filename)
		{
			throw new NotImplementedException ();
		}

		public Paytable Generate ()
		{
			throw new NotImplementedException ();
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
		}
	}
}