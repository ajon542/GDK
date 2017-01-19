using UnityEngine;
using System.Collections;
using System.Threading;

using GDK.MathEngine;
using GDK.MathEngine.Evaluators;

namespace GDK.GameSimulation
{
	public class Simulation : MonoBehaviour
	{
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

		private int bet = 1;
		private int totalBet;
		private int totalWin;

		private int displayCount = 100000;
		private int numberOfSimulations = 1000000;

		private void Simulate()
		{
			while (numberOfSimulations-- > 0)
			{
				totalBet += bet;

				SlotResults results = evaluator.Evaluate (paytable, rng);

				foreach (var result in results.Results)
				{
					totalWin += result.PayCombo.PayAmount;
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
		}
	}
}
