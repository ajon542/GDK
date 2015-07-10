using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<Symbol> Symbols { get; private set; }

        /// <summary>
        /// Gets the pay amount for this pay combination.
        /// </summary>
        public int PayAmount { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayCombo"/> class.
        /// </summary>
        public PayCombo(List<Symbol> symbols, int payAmount)
        {
            Symbols = symbols;
            PayAmount = payAmount;
        }
    }

    /// <summary>
    /// Represents a set, or group of pay combinations.
    /// </summary>
    public class PayComboGroup
    {
        /// <summary>
        /// Gets the paylines in the payline group.
        /// </summary>
        public List<PayCombo> Combos { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayComboGroup"/> class.
        /// </summary>
        public PayComboGroup()
        {
            Combos = new List<PayCombo>();
        }

        /// <summary>
        /// Adds a pay combo to the pay combo group.
        /// </summary>
        /// <param name="payCombo">The payline to add.</param>
        public void AddPayCombo(PayCombo payCombo)
        {
            Combos.Add(payCombo);
        }
    }
}
