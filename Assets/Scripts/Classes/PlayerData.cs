using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public enum GamePhase
    {
        FirstExamination,
        FiveFactorQuestion,
        Learning,
        FinalExamination,
        Completed
    }
    public string playerName
    {
        get;
        set;
    }

    public Dictionary<FiveFactorQuestionManager.PlayerType, int> playerTypePercentages
    {
        get;
        set;
    }

    public GamePhase gamePhase
    {
        get;
        set;
    }

    public int level
    {
        get;
        set;
    }

    public int exp
    {
        get;
        set;
    }

    //コンストラクタ
    public PlayerData()
    {
        playerName = PlayerPrefs.GetString("Player Name", "");

        playerTypePercentages = new Dictionary<FiveFactorQuestionManager.PlayerType, int>();
        foreach(FiveFactorQuestionManager.PlayerType playerType in Enum.GetValues(typeof(FiveFactorQuestionManager.PlayerType)))
        {
            playerTypePercentages.Add(playerType, 0);
        }

        gamePhase = (GamePhase)PlayerPrefs.GetInt("Game Phase", (int)GamePhase.FirstExamination);

        level = PlayerPrefs.GetInt("Level", 1);
        exp = PlayerPrefs.GetInt("Exp", 0);
    }
}
