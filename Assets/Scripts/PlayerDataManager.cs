using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance
    {
        get;
        private set;
    }

    public PlayerData playerData
    {
        get;
        private set;
    }

    [field: SerializeField, RenameField("Player Name Text")]
    private TextMeshProUGUI playerNameText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Aesthietic Percentage Text")]
    private TextMeshProUGUI aesthieticPercentageText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Narrative Percentage Text")]
    private TextMeshProUGUI narrativePercentageText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Goal Percentage Text")]
    private TextMeshProUGUI goalPercentageText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Social Percentage Text")]
    private TextMeshProUGUI socialPercentageText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Challenge Percentage Text")]
    private TextMeshProUGUI challengePercentageText
    {
        get;
        set;
    }

    private void Awake()
    {
        CreateInstance();
        CreatePlayerData();
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void CreatePlayerData()
    {
        if (playerData == null)
        {
            playerData = new PlayerData();
        }
    }

    private void Initialize()
    {
        if(playerData.playerName != "")
        {
            playerNameText.text = playerData.playerName;
        }
        else
        {
            playerNameText.text = "-";
        }

        if(PlayerData.GamePhase.FiveFactorQuestion < playerData.gamePhase)
        {
            foreach (FiveFactorQuestionManager.PlayerType playerType in Enum.GetValues(typeof(FiveFactorQuestionManager.PlayerType)))
            {
                switch (playerType)
                {
                    case FiveFactorQuestionManager.PlayerType.Aesthietic:
                        aesthieticPercentageText.text = playerData.playerTypePercentages[playerType] + "%";
                        break;
                    case FiveFactorQuestionManager.PlayerType.Narrative:
                        narrativePercentageText.text = playerData.playerTypePercentages[playerType] + "%";
                        break;
                    case FiveFactorQuestionManager.PlayerType.Goal:
                        goalPercentageText.text = playerData.playerTypePercentages[playerType] + "%";
                        break;
                    case FiveFactorQuestionManager.PlayerType.Social:
                        socialPercentageText.text = playerData.playerTypePercentages[playerType] + "%";
                        break;
                    case FiveFactorQuestionManager.PlayerType.Challenge:
                        challengePercentageText.text = playerData.playerTypePercentages[playerType] + "%";
                        break;
                }
            }
        }
        else
        {
            aesthieticPercentageText.text = "-";
            narrativePercentageText.text = "-";
            goalPercentageText.text = "-";
            socialPercentageText.text = "-";
            challengePercentageText.text = "-";
        }
        
    }
}
