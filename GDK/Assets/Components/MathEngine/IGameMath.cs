using UnityEngine;
using System.Collections;

namespace GDK.MathEngine
{
    /// <summary>
    /// The interface that games must implement to define the logic and evaluators.
    /// </summary>
    public interface IGameMath
    {
        /// <summary>
        /// Run a single game from base game, bonus features and free games.
        /// </summary>
        /// <returns></returns>
        SlotResults RunOneGame(int bet);
    }
}
