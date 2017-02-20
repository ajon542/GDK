using UnityEditor;
using UnityEngine;

// The view is going to need to:
// - Display config (simulation initialization)
// - Display progress (simulation feedback)
// - Enable/disable buttons (simulation feedback)
// - Display results (simulation feedback)

namespace GDK.GameSimulation
{
    public class SimulationView : EditorWindow
    {
        private SimulationController Controller { get; set; }
        private ISimulationModelData ModelData { get; set; }

        public void Initialize(SimulationController controller, ISimulationModelData modelData)
        {
            Controller = controller;
            ModelData = modelData;
        }

        private void OnGUI()
        {
            ModelData.Name = EditorGUILayout.TextField(ModelData.Name);
            ModelData.NumberOfSimulations = EditorGUILayout.IntField("Number of Simulations", ModelData.NumberOfSimulations);
            ModelData.Bet = EditorGUILayout.IntField("Bet", ModelData.Bet);

            DisplayProgressBar();
            DisplaySimulationButton();
        }

        private void DisplayProgressBar()
        {
            if (Controller.SimulationRunning)
                EditorUtility.DisplayProgressBar(
                    "Simulation Progress", 
                    string.Format("{0}% Complete", Controller.Progress * 100), Controller.Progress);
            else
                EditorUtility.ClearProgressBar();
        }

        private void DisplaySimulationButton()
        {
            if (Controller.SimulationRunning)
            {
                if (GUILayout.Button("Stop Simulation"))
                {
                    Controller.StopSimulation();
                }
            }
            else
            {
                if (GUILayout.Button("Run Simulation"))
                {
                    Controller.RunSimulation();
                }
            }
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        private void OnDestroy()
        {
            Controller.StopSimulation();
        }
    }
}