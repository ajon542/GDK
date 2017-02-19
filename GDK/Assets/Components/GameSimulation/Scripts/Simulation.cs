using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Threading;

using GDK.MathEngine;
using GDK.MathEngine.Evaluators;
using GDK.Utilities;

namespace GDK.GameSimulation
{
    public class Simulation : EditorWindow
    {
        [MenuItem("Window/Simulation")]
        static void Init()
        {
            Simulation window = (Simulation)EditorWindow.GetWindow(typeof(Simulation));
            window.Show();
        }

        public int numberOfSimulations;
        public int bet;

        private float progress;
        private bool simulationComplete;
        private Thread simulationThread1;

        private void OnGUI()
        {
            GUILayout.Label("Simulation Tool");
            numberOfSimulations = EditorGUILayout.IntField("Number of Simulations", numberOfSimulations);
            bet = EditorGUILayout.IntField("Bet", bet);

            if (simulationComplete == false)
                EditorUtility.DisplayProgressBar("Simulation Progress", string.Empty, progress);
            else
                EditorUtility.ClearProgressBar();

            if (GUILayout.Button("Run"))
            {
                simulationThread1 = new Thread(Simulate);
                simulationThread1.Start();
            }
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        private void Simulate()
        {
            PaytableBuilder builder = new SimulationPaytableBuilder();
            Paytable paytable = new Paytable();
            IEvaluator evaluator = new PaylineEvaluator();
            IRng rng = new Rng();

            paytable.ReelGroup = builder.BuildReelGroup();
            paytable.PaylineGroup = builder.BuildPaylineGroup();
            paytable.PayComboGroup = builder.BuildPayComboGroup();

            int currentSimulation = 0;
            int totalBet = 0;
            int totalWin = 0;
            simulationComplete = false;

            while (currentSimulation++ < numberOfSimulations && simulationComplete == false)
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

                progress = (float)currentSimulation / (float)numberOfSimulations;
            }

            Debug.Log(string.Format("TotalWin={0}, TotalBet={1}, RTP={2}",
                totalWin,
                totalBet,
                (float)totalWin / (float)totalBet));

            simulationComplete = true;
        }

        private void OnDestroy()
        {
            simulationComplete = true;
        }
    }
}
