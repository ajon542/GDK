using GDK.MathEngine;

namespace GDK.GameSimulation
{
    public interface ISimulationModelData
    {
        string Name { get; set; }
        int NumberOfSimulations { get; set; }
        int Bet { get; set; }
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
            NumberOfSimulations = 1000;
            Bet = 1;
        }
    }
}