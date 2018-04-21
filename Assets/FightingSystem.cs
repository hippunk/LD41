using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FightingSystem {

	/*public RPS playerChoice;
	private RPS AIChoice;

	private RPS[] randomChoiceTab = { RPS.Rock, RPS.Paper, RPS.Scisors };

	private Result[][] resultTab = {
		{ Result.DRAW, Result.LOSE, Result.WIN },
		{ Result.WIN, Result.DRAW, Result.LOSE },
		{ Result.LOSE, Result.WIN, Result.DRAW }
	};

	public enum Result {
		WIN,
		LOSE,
		DRAW
	}

	public enum RPS {
		Rock = 0,
		Paper = 1,
		Scisors = 2,
		NotPlayed = 3
	}



	public void engageBattle(FightingSystem.RPS AIDecision){
		AIChoice = AIDecision;
	}

	public void playRock(){
		playerChoice = RPS.Rock;
	}

	public void playPaper(){
		playerChoice = RPS.Paper;
	}

	public void playScisors(){
		playerChoice = RPS.Scisors;
	}

	public Result hasWon(){
		Result returned;

		if (playerChoice == RPS.NotPlayed)
			return Result.DRAW;

		if (AIChoice == RPS.NotPlayed)
			AIChoice = randomChoiceTab[(int) Random.Range (0, 2)];

		returned = resultTab [playerChoice] [AIChoice];

		playerChoice = RPS.NotPlayed;
		AIChoice = RPS.NotPlayed;
	
		return returned;
	}*/
}
