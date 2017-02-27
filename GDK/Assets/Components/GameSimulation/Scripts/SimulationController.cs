using System.Collections.Generic;
using System.Threading; 
using UnityEditor;
using UnityEngine;

using GDK.MathEngine;
using GDK.MathEngine.Evaluators;

namespace GDK.GameSimulation
{
    /// <summary>
    /// The simulation controller.
    /// </summary>
    public class SimulationController
    {
        private static SimulationController controller;
        private static ISimulationModelData modelData;
        private Thread simulationThread;

        [MenuItem("Window/Simulation")]
        private static void Init()
        {
            controller = new SimulationController();
            modelData = new SimulationModelData();

            SimulationView window = (SimulationView)EditorWindow.GetWindow(typeof(SimulationView));
            window.Initialize(controller, modelData);
            window.Show();
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

            // Start the simulation timer.
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            // Create the paytrable used in the simulation.
            IEvaluator evaluator = new PaylineEvaluator();
            IRng rng = new Rng();
            PaytableBuilder builder = new SimulationPaytableBuilder();
            Paytable paytable = new Paytable();
            paytable.BaseGameReelGroup = builder.BuildReelGroup();
            paytable.PaylineGroup = builder.BuildPaylineGroup();
            paytable.PayComboGroup = builder.BuildPayComboGroup();

            int totalBet = 0;
            int totalWin = 0;
            int currentSimulation = 0;

            // Run the simulation.
            while (currentSimulation < modelData.NumberOfSimulations && SimulationRunning)
            {
                totalBet += modelData.Bet;

                // Generate the random numbers.
                List<int> randomNumbers = new List<int>();
                for (int reel = 0; reel < paytable.BaseGameReelGroup.Reels.Count; ++reel)
                {
                    ReelStrip reelStrip = paytable.BaseGameReelGroup.Reels[reel].ReelStrip;
                    randomNumbers.Add(rng.GetRandomNumber(reelStrip.Symbols.Count));
                }

                ReelWindow reelWindow = new ReelWindow(paytable.BaseGameReelGroup, randomNumbers);

                SlotResult result = evaluator.Evaluate(paytable, reelWindow, rng);

                totalWin += result.TotalWin;

                Progress = (float)currentSimulation / (float)modelData.NumberOfSimulations;
                currentSimulation++;
            }

            // Display the results.
            Debug.Log(string.Format("TotalWin={0}, TotalBet={1}, RTP={2}",
                totalWin,
                totalBet,
                (float)totalWin / (float)totalBet));

            // Display the simulation time.
            System.TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Debug.Log("RunTime " + elapsedTime);

            SimulationRunning = false;
        }
    }
}