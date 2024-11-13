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

        SaveResult(score, maximumScore,level);
    }

    private void ObtainExp(int score, int maximumScore, ExaminationManager.ExaminationLevel level)
    {
        if (ExaminationManager.ExaminationLevel.All == level)
        {
            return;
        }
        else
        {
            int moreExp = expData.GetNeedForLvUpExp(PlayerDataManager.instance.playerData.level + 1);
            PlayerDataManager.instance.playerData.exp += expData.CalcExp(score, maximumScore, level);
            if (moreExp <= PlayerDataManager.instance.playerData.exp)
            {
                PlayerDataManager.instance.playerData.level++;
            }
        }
    }

    private void SaveResult(int score, int maximumScore, ExaminationManager.ExaminationLevel level)
    {
        ObtainExp(score, maximumScore, level);

        if (PlayerDataManager.instance.playerData.gamePhase == PlayerData.GamePhase.FirstExamination)
        {
            PlayerDataManager.instance.playerData.gamePhase = PlayerData.GamePhase.FiveFactorQuestion;
            PlayerDataManager.instance.playerData.scoreBeforeLearning = score;
        }
        else if(PlayerDataManager.instance.playerData.gamePhase == PlayerData.GamePhase.FinalExamination)
        {
            PlayerDataManager.instance.playerData.gamePhase = PlayerData.GamePhase.Completed;
            PlayerDataManager.instance.playerData.scoreAfterLearning = score;
            PlayerDataManager.instance.SaveCsvPlayerData();
        }
        else if(PlayerDataManager.instance.playerData.gamePhase == PlayerData.GamePhase.Learning)
        {
            if(ExaminationManager.ExaminationLevel.All == level)
            {
                PlayerDataManager.instance.playerData.examinationTakeCount++;

                //scoreによるアチーブメント解放
                if (10 <= score)
                {
                    AchievementManager.instance.UnlockAchievement(7);
                }
                if (20 <= score)
                {
                    AchievementManager.instance.UnlockAchievement(8);
                }
                if (30 <= score)
                {
                    AchievementManager.instance.UnlockAchievement(9);
                }
                if (40 <= score)
                {
                    AchievementManager.instance.UnlockAchievement(10);
                }
                if (score == 50)
                {
                    AchievementManager.instance.UnlockAchievement(11);
                }

                //試験回数によるアチーブメント解放
                if (5 <= PlayerDataManager.instance.playerData.examinationTakeCount)
                {
                    AchievementManager.instance.UnlockAchievement(3);
                }
                else if (3 <= PlayerDataManager.instance.playerData.examinationTakeCount)
                {
                    AchievementManager.instance.UnlockAchievement(2);
                }
                else if (1 <= PlayerDataManager.instance.playerData.examinationTakeCount)
                {
                    AchievementManager.instance.UnlockAchievement(1);
                }
            }
        }

        AchievementManager.instance.CntUnlockAchivement();
    }
}
