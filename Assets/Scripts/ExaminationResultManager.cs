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
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ShowResult(int score, int maximumScore)
    {
        scoreText.text = score.ToString();
        maximumScoreText.text = "/" + maximumScore;

        SaveResult(score);
    }

    private void SaveResult(int score)
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
