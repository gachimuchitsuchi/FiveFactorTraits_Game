using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FiveFactorQuestionResultManager : MonoBehaviour
{
    public static FiveFactorQuestionResultManager instance
    {
        get;
        private set;
    }

    [field: SerializeField, RenameField("Result List")]
    private GameObject resultList
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("FiveFactorQuestionResult Prefab")]
    private GameObject ffqResultPrefab
    {
        get;
        set;
    }

    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ShowResult(Dictionary<FiveFactorQuestionManager.PlayerType, int> playerTypePercentages)
    {
        foreach(FiveFactorQuestionManager.PlayerType playerType in Enum.GetValues(typeof(FiveFactorQuestionManager.PlayerType)))
        {
            GameObject ffqResult = null;
            if(playerType != FiveFactorQuestionManager.PlayerType.None)
            {
                ffqResult = Instantiate(ffqResultPrefab, resultList.transform);
            }

            switch (playerType)
            {
                case FiveFactorQuestionManager.PlayerType.Aesthietic:
                    ffqResult.GetComponentInChildren<TextMeshProUGUI>().text = "���I�u��: ";
                    break;
                case FiveFactorQuestionManager.PlayerType.Narrative:
                    ffqResult.GetComponentInChildren<TextMeshProUGUI>().text = "����u��: ";
                    break;
                case FiveFactorQuestionManager.PlayerType.Goal:
                    ffqResult.GetComponentInChildren<TextMeshProUGUI>().text = "�ړI�u��: ";
                    break;
                case FiveFactorQuestionManager.PlayerType.Social:
                    ffqResult.GetComponentInChildren<TextMeshProUGUI>().text = "�Љ�I�u��: ";
                    break;
                case FiveFactorQuestionManager.PlayerType.Challenge:
                    ffqResult.GetComponentInChildren<TextMeshProUGUI>().text = "����u��: ";
                    break;
                case FiveFactorQuestionManager.PlayerType.None:
                    break;
            }
            if(playerType != FiveFactorQuestionManager.PlayerType.None)
            {
                ffqResult.GetComponentInChildren<TextMeshProUGUI>().text += playerTypePercentages[playerType] + "%";
            }
        }
        SaveResult(playerTypePercentages);
    }

    private void SaveResult(Dictionary<FiveFactorQuestionManager.PlayerType, int> playerTypePercentages)
    {
        PlayerDataManager.instance.playerData.playerTypePercentages = playerTypePercentages;
        PlayerDataManager.instance.playerData.gamePhase = PlayerData.GamePhase.Learning;
    }
}
