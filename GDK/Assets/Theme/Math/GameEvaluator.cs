using UnityEngine;
using System.Collections.Generic;
using GDK.MathEngine;
using GDK.MathEngine.Evaluators;

public class GameEvaluator : IEvaluator
{
    /// <summary>
    /// The theme specific game evaluation logic.
    /// </summary>
    public SlotResults Evaluate(Paytable paytable, ReelWindow reelWindow, IRng rng)
    {
        // Create the slot results.
        SlotResults slotResults = new SlotResults();

        // Create the evaluators for the base game.
        PaylineEvaluator bgPaylineEval = new PaylineEvaluator();
        ScatterEvaluator bgScatterEval = new ScatterEvaluator();

        // Create the evaluators for the free games.
        PaylineEvaluator fgPaylineEval = new PaylineEvaluator();
        ScatterEvaluator fgScatterEval = new ScatterEvaluator();

        // Generate the random numbers.
        List<int> randomNumbers = new List<int>();
        for (int reel = 0; reel < paytable.ReelGroup.Reels.Count; ++reel)
        {
            ReelStrip reelStrip = paytable.ReelGroup.Reels[reel].ReelStrip;
            randomNumbers.Add(rng.GetRandomNumber(reelStrip.Symbols.Count));
        }

        reelWindow = new ReelWindow(paytable.ReelGroup, randomNumbers);

        // Evaluate the base game (payline and scatter evaluation).
        SlotResults paylineResults = bgPaylineEval.Evaluate(paytable, reelWindow, rng);
        SlotResults scatterResults = bgScatterEval.Evaluate(paytable, reelWindow, rng);
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
                // Generate the random numbers.
                randomNumbers = new List<int>();
                for (int reel = 0; reel < paytable.ReelGroup.Reels.Count; ++reel)
                {
                    ReelStrip reelStrip = paytable.ReelGroup.Reels[reel].ReelStrip;
                    randomNumbers.Add(rng.GetRandomNumber(reelStrip.Symbols.Count));
                }

                reelWindow = new ReelWindow(paytable.ReelGroup, randomNumbers);

                // Evaluate the free game (payline and scatter evaluation).
                SlotResults fgPaylineResults = fgPaylineEval.Evaluate(paytable, reelWindow, rng);
                SlotResults fgScatterResults = fgScatterEval.Evaluate(paytable, reelWindow, rng);
                slotResults.Results.AddRange(fgPaylineResults.Results);
                slotResults.Results.AddRange(fgScatterResults.Results);

                slotResults.TotalWin += paylineResults.TotalWin;
                slotResults.TotalWin += scatterResults.TotalWin;
            }
        }

        return slotResults;
    }
}
