using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEngine
{
    /// <summary>
    /// Container for properties of a reel.
    /// </summary>
    public class ReelProperties
    {
        /// <summary>
        /// Gets or sets the reel.
        /// </summary>
        public ReelStrip Reel { get; set; }

        /// <summary>
        /// Gets or sets the height of the reel window.
        /// </summary>
        public int Height { get; set; }
    }

    /// <summary>
    /// Represents a set, or a group of reels.
    /// </summary>
    /// <remarks>
    /// A standard 3x5 ReelGroup used by many slot machine games looks like the following:
    ///
    ///     0  [ ][ ][ ][ ][ ]  \
    ///     1  [ ][ ][ ][ ][ ]   |--- Height
    ///     2  [ ][ ][ ][ ][ ]  /
    ///
    /// In order to provide a more flexible reel group window, we can specify the height
    /// of each reel individually so we can acheive the following:
    ///
    ///     0  [ ][ ][ ][ ][ ]
    ///     1  [ ]   [ ][ ][ ]
    ///     2  [ ]   [ ]   [ ]
    ///     3        [ ]   [ ]
    ///     4              [ ]
    /// 
    /// </remarks>
    public class ReelGroup
    {
        /// <summary>
        /// Gets the reels in the reel group.
        /// </summary>
        public List<ReelProperties> Reels { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReelGroup"/> class.
        /// </summary>
        public ReelGroup()
        {
            Reels = new List<ReelProperties>();
        }

        /// <summary>
        /// Adds a reel to the reel group.
        /// </summary>
		/// <param name="reelStrip">The reel strip to add.</param>
        /// <param name="reelHeight">The height of the reel window.</param>
        public void AddReel(ReelStrip reelStrip, int reelHeight = 3)
        {
            Reels.Add(new ReelProperties { Reel = reelStrip, Height = reelHeight });
        }
    }
}
