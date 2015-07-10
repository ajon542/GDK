using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    /// <summary>
    /// Base class for all trigger objects.
    /// </summary>
    public class Trigger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trigger"/> class.
        /// </summary>
        /// <param name="name">The name of the trigger.</param>
        public Trigger(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of the trigger.
        /// </summary>
        public string Name { get; private set; }
    }
}
