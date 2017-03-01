using UnityEditor;
using UnityEngine;

using GDK.MathEngine;
using GDK.GameSimulation;

public class ThemeSimulation
{
    [MenuItem("Window/Simulation")]
    public static void Init()
    {
        GameMath gameMath = new GameMath();
        gameMath.Init();
        SimulationController.Init(gameMath);
    }
}