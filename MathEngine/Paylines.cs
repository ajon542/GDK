using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEngine
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
        /// Get sor sets the offset into the reel window.
        /// </summary>
        public int Offset { get; set; }
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
        public List<PaylineCoord> PaylineCoordList { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Payline"/> class.
        /// </summary>
        public Payline()
        {
            PaylineCoordList = new List<PaylineCoord>();
        }

        /// <summary>
        /// Add a payline cooredinate to the payline.
        /// </summary>
        /// <param name="coord">The payline coordinate.</param>
        public void AddPaylineCoord(PaylineCoord coord)
        {
            PaylineCoordList.Add(coord);
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
        public PaylineGroup()
        {
            Paylines = new List<Payline>();
        }

        /// <summary>
        /// Adds a payline to the payline group.
        /// </summary>
        /// <param name="payline">The payline to add.</param>
        public void AddPayline(Payline payline)
        {
            Paylines.Add(payline);
        }
    }
}
