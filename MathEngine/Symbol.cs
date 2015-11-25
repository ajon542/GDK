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
        public int Id { get; set; }

        /// <summary>
        /// Gets the name of the symbol. The name will most likely be useful when debugging.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        public Symbol()
        {
        }

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

        /// <summary>
        /// String representation of the Symbol.
        /// </summary>
        /// <returns>A string representation of the Symbol.</returns>
        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Symbol p = obj as Symbol;
            if (p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Name == p.Name) && (Id == p.Id);
        }

        public bool Equals(Symbol p)
        {
            // If parameter is null return false:
            if (p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Name == p.Name) && (Id == p.Id);
        }

        public override int GetHashCode()
        {
            int hashName = Name.GetHashCode();
            int hashId = Id.GetHashCode();

            //Calculate the hash code for the symbol. 
            return hashName ^ hashId;
        }
    }
}
