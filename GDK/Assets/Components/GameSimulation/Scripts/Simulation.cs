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
		private PaytableBuilder builder = new SimulationPaytableBuilder ();
		private Paytable paytable = new Paytable ();
		private IEvaluator evaluator = new PaylineEvaluator ();
		private IRng rng = new Rng ();
		private Thread simulationThread;

		private void Start ()
		{
			paytable.ReelGroup = builder.BuildReelGroup ();
			paytable.PaylineGroup = builder.BuildPaylineGroup ();
			paytable.PayComboGroup = builder.BuildPayComboGroup ();

			simulationThread = new Thread (Simulate);
			simulationThread.Start ();
		}

		private int bet = 5;
		private int totalBet;
		private int totalWin;

		private int displayCount = 100000;
		private int numberOfSimulations = 1000000;
        private bool simulationComplete = false;

		private void Simulate()
		{
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            while (numberOfSimulations-- > 0 && simulationComplete == false)
			{
				totalBet += bet;

				SlotResults results = evaluator.Evaluate (paytable, rng);

                var component = results.Results[0].GetComponent<PaylinesComponent>();
                if (component != null)
                {
                    foreach (var result in component.PayResults)
                    {
                        totalWin += result.PayCombo.PayAmount;
                    }
                }

				displayCount--;
				if (displayCount == 0)
				{
					Debug.Log (string.Format ("TotalWin={0}, TotalBet={1}, RTP={2}", 
						totalWin, 
						totalBet, 
						(float)totalWin / (float)totalBet));
					displayCount = 100000;
				}
			}

            // Get the elapsed time as a TimeSpan value.
            System.TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Debug.Log("RunTime " + elapsedTime);
		}

        private void OnDestroy()
        {
            simulationComplete = true;
        }
	}
}
