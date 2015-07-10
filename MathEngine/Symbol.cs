using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEngine
{
    /// <summary>
    /// Represents a symbol.
    /// </summary>
    public class Symbol
    {
        /// <summary>
        /// Gets the identifier for ths symbol. The identifier should be unique.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the name of the symbol. The name will most likely be useful when debugging.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="id">The identifier for the symbol.</param>
        /// <param name="name">The name of the symbol.</param>
        public Symbol(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="other">The symbol to copy.</param>
        public Symbol(Symbol other)
        {
            Id = other.Id;
            Name = other.Name;
        }
    }
}
