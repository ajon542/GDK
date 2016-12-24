using System;
using System.Collections.Generic;
using System.Text;

namespace MathEngine
{
	/// <summary>
	/// Represents a simple left to right pay combination.
	/// </summary>
	public class PayCombo
	{
		/// <summary>
		/// Gets the list of symbols in the pay combination.
		/// </summary>
		public List<Symbol> Symbols { get; set; }

		/// <summary>
		/// Gets the pay amount for this pay combination.
		/// </summary>
		public int PayAmount { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PayCombo"/> class.
		/// </summary>
		public PayCombo ()
		{
			Symbols = new List<Symbol> ();
			PayAmount = 0;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PayCombo"/> class.
		/// </summary>
		/// <param name="symbols">The symbols in the pay combination.</param>
		/// <param name="payAmount">The pay amount.</param>
		public PayCombo (List<Symbol> symbols, int payAmount)
		{
			Symbols = symbols;
			PayAmount = payAmount;
		}

		//public override string ToString()
		//{
		//    return string.Join(", ", Symbols);
		//}
	}

	/// <summary>
	/// Represents a set, or group of pay combinations.
	/// </summary>
	public class PayComboGroup
	{
		/// <summary>
		/// Gets the paylines in the payline group.
		/// </summary>
		public List<PayCombo> Combos { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PayComboGroup"/> class.
		/// </summary>
		public PayComboGroup ()
		{
			Combos = new List<PayCombo> ();
		}

		/// <summary>
		/// Adds a pay combo to the pay combo group.
		/// </summary>
		/// <param name="payCombo">The payline to add.</param>
		public void AddPayCombo (PayCombo payCombo)
		{
			Combos.Add (payCombo);
		}
	}
}
