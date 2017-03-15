using UnityEngine;
using System.Collections.Generic;
using GDK.MathEngine;
using GDK.MathEngine.Evaluators;

public class GameMath : IGameMath
{
    private IRng rng;
    private Paytable paytable;

    public void Init()
    {
        rng = new Rng();
        paytable = new Paytable();
        PaytableBuilder builder = new GamePaytableBuilder();
        paytable.BaseGameReelGroup = builder.BuildBGReelGroup();
        paytable.PaylineGroup = builder.BuildPaylineGroup();
        paytable.PayComboGroup = builder.BuildPayComboGroup();
        paytable.ScatterComboGroup = builder.BuildScatterComboGroup();
    }

    /// <summary>
    /// The theme specific game evaluation logic.
    /// </summary>
    public SlotResults RunOneGame(int bet)
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
        for (int reel = 0; reel < paytable.BaseGameReelGroup.Reels.Count; ++reel)
        {
            ReelStrip reelStrip = paytable.BaseGameReelGroup.Reels[reel].ReelStrip;
            randomNumbers.Add(rng.GetRandomNumber(reelStrip.Symbols.Count));
        }

        ReelWindow reelWindow = new ReelWindow(paytable.BaseGameReelGroup, randomNumbers);

        // Evaluate the base game (payline and scatter evaluation).
        SlotResult paylineResults = bgPaylineEval.Evaluate(paytable, reelWindow, rng);
        SlotResult scatterResults = bgScatterEval.Evaluate(paytable, reelWindow, rng);
        slotResults.Results.Add(paylineResults);
        slotResults.Results.Add(scatterResults);

        slotResults.TotalWin += paylineResults.TotalWin;
        slotResults.TotalWin += scatterResults.TotalWin;

        // Evaluate the free games (if any).
        if (scatterResults.GetComponent<ScattersComponent>() != null)
        {
            // Add total number of free games.
            int freeGamesAwarded = 5;

            for (int i = 0; i < freeGamesAwarded; ++i)
            {
                // Generate the random numbers.
                randomNumbers = new List<int>();
                for (int reel = 0; reel < paytable.FreeGamesReelGroup.Reels.Count; ++reel)
                {
                    ReelStrip reelStrip = paytable.FreeGamesReelGroup.Reels[reel].ReelStrip;
                    randomNumbers.Add(rng.GetRandomNumber(reelStrip.Symbols.Count));
                }

                reelWindow = new ReelWindow(paytable.FreeGamesReelGroup, randomNumbers);

                // Evaluate the free game (payline and scatter evaluation).
                SlotResult fgPaylineResults = fgPaylineEval.Evaluate(paytable, reelWindow, rng);
                SlotResult fgScatterResults = fgScatterEval.Evaluate(paytable, reelWindow, rng);
                slotResults.Results.Add(fgPaylineResults);
                slotResults.Results.Add(fgScatterResults);

                slotResults.TotalWin += paylineResults.TotalWin;
                slotResults.TotalWin += scatterResults.TotalWin;
            }
        }

        return slotResults;
    }
}
