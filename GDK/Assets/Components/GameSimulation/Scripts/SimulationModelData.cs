using GDK.MathEngine;

namespace GDK.GameSimulation
{
    /// <summary>
    /// Data associated with the simulation.
    /// </summary>
    public interface ISimulationModelData
    {
        string Name { get; set; }

        /// <summary>
        /// The number of games to simulate.
        /// </summary>
        int NumberOfSimulations { get; set; }
        
        /// <summary>
        /// The bet amount for the simulation.
        /// </summary>
        int Bet { get; set; }

        /// <summary>
        /// The entire results of the simulation.
        /// </summary>
        SlotResults SlotResults { get; set; }
    }

    public class SimulationModelData : ISimulationModelData
    {
        public string Name { get; set; }
        public int NumberOfSimulations { get; set; }
        public int Bet { get; set; }
        public SlotResults SlotResults { get; set; }

        public SimulationModelData()
        {
            Name = "Simulation Name";
            NumberOfSimulations = 100000;
            Bet = 1;
        }
    }
}