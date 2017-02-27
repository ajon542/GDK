using System;
using System.Collections.Generic;

namespace GDK.MathEngine
{
    public interface IComponent { }

    public class PayResult
    {
        public PayCombo PayCombo { get; set; }
        public Payline Payline { get; set; }

        public PayResult()
        {
            PayCombo = new PayCombo();
            Payline = new Payline();
        }
    }

    public class PickResult
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Trigger { get; set; }
    }

    public class PaysComponent : IComponent
    {
        public List<PayResult> PayResults { get; set; }

        public PaysComponent()
        {
            PayResults = new List<PayResult>();
        }
    }

    public class PickComponent : IComponent
    {
        public List<PickResult> PickResults { get; set; }

        public PickComponent()
        {
            PickResults = new List<PickResult>();
        }
    }

    public class PaylinesComponent : PaysComponent { }

    public class ScattersComponent : PaysComponent { }

    public class SlotResult
    {
        private List<IComponent> components { get; set; }

        public int TotalWin { get; set; }

        public SlotResult()
        {
            components = new List<IComponent>();
        }

        /// <summary>
        /// Adds a component to the game object.
        /// </summary>
        /// <typeparam name="T">The type of component to add.</typeparam>
        /// <param name="component">The component to add.</param>
        public void AddComponent<T>(IComponent component) where T : class
        {
            if (GetComponent<T>() == null)
            {
                components.Add(component);
            }
        }

        /// <summary>
        /// Removes a component from the game object.
        /// </summary>
        /// <typeparam name="T">The type of component to remove.</typeparam>
        public void RemoveComponent<T>() where T : class
        {
            for (int i = 0; i < components.Count; ++i)
            {
                if (components[i] is T)
                {
                    components.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Gets a component of type T.
        /// </summary>
        /// <typeparam name="T">The type of component to get.</typeparam>
        /// <returns>The component or null if not found.</returns>
        public T GetComponent<T>() where T : class
        {
            foreach (IComponent component in components)
            {
                if (component is T)
                {
                    return component as T;
                }
            }

            return null;
        }
    }

    public class SlotResults
    {
        public SlotResults()
        {
            Results = new List<SlotResult>();
        }

        public List<SlotResult> Results { get; set; }

        public int TotalWin { get; set; }
    }
}
