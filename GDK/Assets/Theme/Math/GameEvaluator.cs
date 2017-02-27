using UnityEngine;
using System.Collections;
using GDK.MathEngine;
using GDK.MathEngine.Evaluators;

public class GameEvaluator : IEvaluator
{
    /// <summary>
    /// The theme specific game evaluation logic.
    /// </summary>
    public SlotResults Evaluate(Paytable paytable, IRng rng)
    {
        // Create the slot results.
        SlotResults slotResults = new SlotResults();

        // Create the evaluators for the base game.
        PaylineEvaluator bgPaylineEval = new PaylineEvaluator();
        ScatterEvaluator bgScatterEval = new ScatterEvaluator();

        // TODO: This is where it might make sense to be passing in the reel window to the evaluator.
        // If we pass in the reel window, the evaluator no longer has to determine where to pull the
        // reels data from.

        // Create the evaluators for the free games.
        PaylineEvaluator fgPaylineEval = new PaylineEvaluator();
        ScatterEvaluator fgScatterEval = new ScatterEvaluator();

        // Evaluate the base game (payline and scatter evaluation).
        SlotResults paylineResults = bgPaylineEval.Evaluate(paytable, rng);
        SlotResults scatterResults = bgScatterEval.Evaluate(paytable, rng);
        slotResults.Results.AddRange(paylineResults.Results);
        slotResults.Results.AddRange(scatterResults.Results);

        slotResults.TotalWin += paylineResults.TotalWin;
        slotResults.TotalWin += scatterResults.TotalWin;

        // Evaluate the free games (if any).
        if (scatterResults.Results.Count > 0)
        {
            // Add total number of free games.
            int freeGamesAwarded = 5;

            for (int i = 0; i < freeGamesAwarded; ++i)
            {
                // Evaluate the free game (payline and scatter evaluation).
                SlotResults fgPaylineResults = fgPaylineEval.Evaluate(paytable, rng);
                SlotResults fgScatterResults = fgScatterEval.Evaluate(paytable, rng);
                slotResults.Results.AddRange(fgPaylineResults.Results);
                slotResults.Results.AddRange(fgScatterResults.Results);

                slotResults.TotalWin += paylineResults.TotalWin;
                slotResults.TotalWin += scatterResults.TotalWin;
            }
        }

        return slotResults;
    }
}
