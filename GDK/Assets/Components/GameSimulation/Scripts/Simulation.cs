using UnityEngine;
using System.Collections;
using System.Threading;

using GDK.MathEngine;
using GDK.MathEngine.Evaluators;

namespace GDK.GameSimulation
{
    public class Simulation : MonoBehaviour
    {
        // Add number of simulations
        // Add RTP progress
        // Add paytable name or evaluator name
        // Select metrics???
        //private PaytableBuilder builder = new SimulationPaytableBuilder();
        //private Paytable paytable = new Paytable();
        //private IEvaluator evaluator = new PaylineEvaluator();
        //private IRng rng = new Rng();
        private Thread simulationThread1;
        private Thread simulationThread2;
        private Thread simulationThread3;

        private void Start()
        {
            simulationThread1 = new Thread(Simulate);
            simulationThread2 = new Thread(Simulate);
            simulationThread3 = new Thread(Simulate);
            simulationThread1.Start();
            simulationThread2.Start();
            simulationThread3.Start();
        }

        private int bet = 5;
        private bool simulationComplete = false;

        private static int sleepTime = 1;

        private void Simulate()
        {
            PaytableBuilder builder = new SimulationPaytableBuilder();
            Paytable paytable = new Paytable();
            IEvaluator evaluator = new PaylineEvaluator();
            IRng rng = new Rng();

            paytable.ReelGroup = builder.BuildReelGroup();
            paytable.PaylineGroup = builder.BuildPaylineGroup();
            paytable.PayComboGroup = builder.BuildPayComboGroup();

            int numberOfSimulations = 1000000;
            int displayCount = 100000;
            int totalBet = 0;
            int totalWin = 0;

            while (numberOfSimulations-- > 0 && simulationComplete == false)
            {
                totalBet += bet;

                SlotResults results = evaluator.Evaluate(paytable, rng);

                var component = results.Results[0].GetComponent<PaylinesComponent>();
                if (component != null)
                {
                    foreach (var result in component.PayResults)
                    {
                        totalWin += result.PayCombo.PayAmount;
                    }
                }

                /*displayCount--;
                if (displayCount == 0)
                {
                    Debug.Log (string.Format ("TotalWin={0}, TotalBet={1}, RTP={2}", 
                        totalWin, 
                        totalBet, 
                        (float)totalWin / (float)totalBet));
                    displayCount = 100000;
                }*/
            }

            Debug.Log(string.Format("TotalWin={0}, TotalBet={1}, RTP={2}",
                totalWin,
                totalBet,
                (float)totalWin / (float)totalBet));

            // Get the elapsed time as a TimeSpan value.
            /*System.TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Debug.Log("RunTime " + elapsedTime);*/
        }

        private void OnDestroy()
        {
            simulationComplete = true;
        }
    }
}
