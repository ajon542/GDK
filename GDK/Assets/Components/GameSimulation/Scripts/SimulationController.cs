using System.Threading; 
using UnityEditor;
using UnityEngine;

using GDK.MathEngine;
using GDK.MathEngine.Evaluators;

namespace GDK.GameSimulation
{
    public class SimulationController
    {
        private static SimulationController controller;
        private static ISimulationModelData modelData;
        private Thread simulationThread;

        [MenuItem("Window/Simulation")]
        private static void Init()
        {
            SimulationView window = (SimulationView)EditorWindow.GetWindow(typeof(SimulationView));
            window.Show();

            controller = new SimulationController();
            modelData = new SimulationModelData();
            window.Initialize(controller, modelData);
        }

        public bool SimulationRunning { get; private set; }
        public float Progress { get; private set; }

        public void RunSimulation()
        {
            if (SimulationRunning)
                return;
            Debug.Log("Running Simulation...");

            simulationThread = new Thread(Simulate);
            simulationThread.Start();
        }

        public void StopSimulation()
        {
            Debug.Log("Stop Simulation");
            SimulationRunning = false;
        }

        private void Simulate()
        {
            SimulationRunning = true;
            PaytableBuilder builder = new SimulationPaytableBuilder();
            Paytable paytable = new Paytable();
            IEvaluator evaluator = new PaylineEvaluator();
            IRng rng = new Rng();

            paytable.ReelGroup = builder.BuildReelGroup();
            paytable.PaylineGroup = builder.BuildPaylineGroup();
            paytable.PayComboGroup = builder.BuildPayComboGroup();

            int totalBet = 0;
            int totalWin = 0;
            int currentSimulation = 0;

            while (currentSimulation < modelData.NumberOfSimulations && SimulationRunning)
            {
                totalBet += modelData.Bet;

                SlotResults results = evaluator.Evaluate(paytable, rng);

                var component = results.Results[0].GetComponent<PaylinesComponent>();
                if (component != null)
                {
                    foreach (var result in component.PayResults)
                    {
                        totalWin += result.PayCombo.PayAmount;
                    }
                }

                Progress = (float)currentSimulation / (float)modelData.NumberOfSimulations;
                currentSimulation++;
            }

            Debug.Log(string.Format("TotalWin={0}, TotalBet={1}, RTP={2}",
                totalWin,
                totalBet,
                (float)totalWin / (float)totalBet));

            SimulationRunning = false;
        }
    }
}