using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    /// <summary>
    /// Base class for all state objects.
    /// </summary>
    public abstract class BaseState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseState"/> class.
        /// </summary>
        /// <param name="name">The name of the state.</param>
        public BaseState(string name)
        {
            Name = name;
        }

        public abstract void Init(GameStateMachine stateMachine);

        /// <summary>
        /// Gets the name of the state.
        /// </summary>
        public string Name { get; private set; }
    }
}
