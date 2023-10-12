using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExaminationResultManager : MonoBehaviour
{
    public static ExaminationResultManager instance
    {
        get;
        private set;
    }

    public ExpData expData
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("ScoreText")]
    private TextMeshProUGUI scoreText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Maximum Score Text")]
    private TextMeshProUGUI maximumScoreText
    {
        get;
        set;
    }

    private void Awake()
    {
        CreateInstance();
        CreateExpData();
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void CreateExpData()
    {
        if(expData == null)
        {
            expData = new ExpData();
        }
    }
    public void ShowResult(int score, int maximumScore, ExaminationManager.ExaminationLevel level)
    {
        scoreText.text = score.ToString();
        maximumScoreText.text = "/" + maximumScore;

        if (ExaminationManager.ExaminationLevel.All == level)
        {

        }
        else
        {
            int moreExp = expData.GetNeedForLvUpExp(PlayerDataManager.instance.playerData.level + 1);
            PlayerDataManager.instance.playerData.exp += expData.CulcExp(score, maximumScore, level);
            if(moreExp <= PlayerDataManager.instance.playerData.exp)
            {
                PlayerDataManager.instance.playerData.level++;
            }
        }

        SaveResult();
    }

    private void SaveResult()
    {
        if(PlayerDataManager.instance.playerData.gamePhase == PlayerData.GamePhase.FirstExamination)
        {
            PlayerDataManager.instance.playerData.gamePhase = PlayerData.GamePhase.FiveFactorQuestion;
        }
        else if(PlayerDataManager.instance.playerData.gamePhase == PlayerData.GamePhase.FinalExamination)
        {
            PlayerDataManager.instance.playerData.gamePhase = PlayerData.GamePhase.Completed;
        }
    }
}
