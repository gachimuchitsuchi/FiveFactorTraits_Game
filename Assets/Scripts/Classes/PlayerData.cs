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
    
    //コンストラクタ
    public PlayerData()
    {
        playerName = "";

        playerTypePercentages = new Dictionary<FiveFactorQuestionManager.PlayerType, int>();
        foreach(FiveFactorQuestionManager.PlayerType playerType in Enum.GetValues(typeof(FiveFactorQuestionManager.PlayerType)))
        {
            playerTypePercentages.Add(playerType, 0);
        }

    }
}
