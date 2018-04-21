using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FightingSystem
{

    public RPS playerChoice;
    private RPS AIChoice;

    private RPS[] randomChoiceTab = { RPS.Rock, RPS.Paper, RPS.Scisors };

    private Result[,] resultTab;

    public enum Result
    {
        WIN,
        LOSE,
        DRAW
    }

    public enum RPS
    {
        Rock = 0,
        Paper = 1,
        Scisors = 2,
        NotPlayed = 3
    }

    public FightingSystem()
    {
        resultTab = new Result[3, 3];
        resultTab[(int)RPS.Rock, (int)RPS.Rock] = Result.DRAW;
        resultTab[(int)RPS.Rock, (int)RPS.Paper] = Result.LOSE;
        resultTab[(int)RPS.Rock, (int)RPS.Scisors] = Result.WIN;
        resultTab[(int)RPS.Paper, (int)RPS.Rock] = Result.WIN;
        resultTab[(int)RPS.Paper, (int)RPS.Paper] = Result.DRAW;
        resultTab[(int)RPS.Paper, (int)RPS.Scisors] = Result.LOSE;
        resultTab[(int)RPS.Scisors, (int)RPS.Rock] = Result.LOSE;
        resultTab[(int)RPS.Scisors, (int)RPS.Paper] = Result.WIN;
        resultTab[(int)RPS.Scisors, (int)RPS.Scisors] = Result.DRAW;
    }

    public void engageBattle(FightingSystem.RPS AIDecision)
    {
        AIChoice = AIDecision;
    }

    public void playRock()
    {
        playerChoice = RPS.Rock;
    }

    public void playPaper()
    {
        playerChoice = RPS.Paper;
    }

    public void playScisors()
    {
        playerChoice = RPS.Scisors;
    }

    public Result hasWon()
    {
        Result returned;

        if (playerChoice == RPS.NotPlayed)
            return Result.DRAW;

        if (AIChoice == RPS.NotPlayed)
            AIChoice = randomChoiceTab[(int)Random.Range(0, 2)];

        returned = resultTab[(int)playerChoice, (int)AIChoice];

        playerChoice = RPS.NotPlayed;
        AIChoice = RPS.NotPlayed;

        return returned;
    }

    public void Testing()
    {
        Result testResult;

        try
        {
            playerChoice = RPS.Rock;
            AIChoice = RPS.Scisors;
            Assert.AreEqual(hasWon(), Result.WIN);

            playerChoice = RPS.Rock;
            AIChoice = RPS.Rock;
            Assert.AreEqual(hasWon(), Result.DRAW);

            playerChoice = RPS.Rock;
            AIChoice = RPS.Paper;
            Assert.AreEqual(hasWon(), Result.LOSE);

            playerChoice = RPS.Paper;
            AIChoice = RPS.Rock;
            Assert.AreEqual(hasWon(), Result.WIN);

        }
        catch (AssertionException e)
        {
            Debug.Log("RPS Fail");
        }

        playerChoice = RPS.NotPlayed;
        AIChoice = RPS.NotPlayed;
    }

}
