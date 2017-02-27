using UnityEditor;
using UnityEngine;

namespace GDK.GameSimulation
{
    /// <summary>
    /// - Display config (simulation initialization)
    /// - Display progress (simulation feedback)
    /// - Enable/disable buttons (simulation feedback)
    /// - Display results (simulation feedback)
    /// </summary>
    public class SimulationView : EditorWindow
    {
        private SimulationController Controller { get; set; }
        private ISimulationModelData ModelData { get; set; }
        private bool initialized;

        /// <summary>
        /// Initialize the view with the controller and model data.
        /// </summary>
        /// <param name="controller">The simulation controller.</param>
        /// <param name="modelData">The data needed to display.</param>
        public void Initialize(SimulationController controller, ISimulationModelData modelData)
        {
            Controller = controller;
            ModelData = modelData;
            initialized = true;
        }

        /// <summary>
        /// Display the view.
        /// </summary>
        private void OnGUI()
        {
            if (!initialized)
                return;

            ModelData.Name = EditorGUILayout.TextField(ModelData.Name);
            ModelData.NumberOfSimulations = EditorGUILayout.IntField("Number of Simulations", ModelData.NumberOfSimulations);
            ModelData.Bet = EditorGUILayout.IntField("Bet", ModelData.Bet);

            DisplayProgressBar();
            DisplaySimulationButton();
        }

        /// <summary>
        /// Display the simulation progress.
        /// </summary>
        private void DisplayProgressBar()
        {
            if (Controller.SimulationRunning)
                EditorUtility.DisplayProgressBar(
                    "Simulation Progress", 
                    string.Format("{0}% Complete", Controller.Progress * 100), Controller.Progress);
            else
                EditorUtility.ClearProgressBar();
        }

        /// <summary>
        /// Display the "run" or "stop" simulation button.
        /// </summary>
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

        /// <summary>
        /// Repaint the display (so the progress bar updates).
        /// </summary>
        private void OnInspectorUpdate()
        {
            Repaint();
        }

        /// <summary>
        /// Cancel the simulation.
        /// </summary>
        private void OnDestroy()
        {
            Controller.StopSimulation();
        }
    }
}