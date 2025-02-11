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

    public enum Group
    {
        A,
        B,
        C,
        None
    }
    public string playerName
    {
        get;
        set;
    }

    /*
    public bool isUsingGameElements
    {
        get;
        set;
    }
    */

    public Dictionary<FiveFactorQuestionManager.PlayerType, int> playerTypePercentages
    {
        get;
        set;
    }

    public Dictionary<FiveFactorQuestionManager.PlayerType, bool> isActiveGameElements
    {
        get;
        set;
    }
    
    public int scoreBeforeLearning
    {
        get;
        set;
    }

    public int scoreAfterLearning
    {
        get;
        set;
    }

    public Group group
    {
        get;
        set;
    }

    public GamePhase gamePhase
    {
        get;
        set;
    }

    public float playTime
    {
        get;
        set;
    }

    public int bossPlayCnt
    {
        get;
        set;
    }

    public int typPlayCnt
    {
        get;
        set;
    }

    public int unlockAchiveCnt
    {
        get;
        set;
    }

    public int level
    {
        get;
        set;
    }

    private const int NUMBER_OF_LEVELS = 5;
    public List<bool> isCorrectAllWordsPerLevel
    {
        get;
        set;
    }

    public int exp
    {
        get;
        set;
    }

    public int examinationTakeCount
    {
        get;
        set;
    }

    public Dictionary<Achievement, bool> unlockedAchievements
    {
        get;
        set;
    }

    public Dictionary<Word, bool> correctlyAnsweredWords
    {
        get;
        set;
    }

    //コンストラクタ
    public PlayerData()
    {
        playerName = PlayerPrefs.GetString("Player Name", "");

        /*
        string savedIsUsingGameElements = PlayerPrefs.GetString("IsUsingGameElements", "");
        if(savedIsUsingGameElements == "")
        {
            isUsingGameElements = true;
        }
        else
        {
            isUsingGameElements = bool.Parse(savedIsUsingGameElements);
        }
        */

        playerTypePercentages = new Dictionary<FiveFactorQuestionManager.PlayerType, int>();
        foreach(FiveFactorQuestionManager.PlayerType playerType in Enum.GetValues(typeof(FiveFactorQuestionManager.PlayerType)))
        {
            playerTypePercentages.Add(playerType, PlayerPrefs.GetInt(playerType + "Percentage", 0));
        }

        isActiveGameElements = new Dictionary<FiveFactorQuestionManager.PlayerType, bool>();
        foreach(FiveFactorQuestionManager.PlayerType playerType in Enum.GetValues(typeof(FiveFactorQuestionManager.PlayerType)))
        {
            if(playerTypePercentages[playerType] >= 60)
            {
                isActiveGameElements.Add(playerType, true);
            }
            else
            {
                isActiveGameElements.Add(playerType, false);
            }
        }

        scoreBeforeLearning = PlayerPrefs.GetInt("Score Before Learning", 0);
        scoreAfterLearning = PlayerPrefs.GetInt("Score After Learning", 0);

        gamePhase = (GamePhase)PlayerPrefs.GetInt("Game Phase", (int)GamePhase.FirstExamination);
        group = (Group)PlayerPrefs.GetInt("Group", (int)Group.None);

        playTime = PlayerPrefs.GetFloat("Play Time", 0.0f);

        bossPlayCnt = PlayerPrefs.GetInt("BossPlayCnt", 0);
        typPlayCnt = PlayerPrefs.GetInt("TypPlayCnt", 0);
        unlockAchiveCnt = PlayerPrefs.GetInt("UnlockAchiveCnt", 0);
        level = PlayerPrefs.GetInt("Level", 1);
        exp = PlayerPrefs.GetInt("Exp", 0);

        examinationTakeCount = PlayerPrefs.GetInt("Examination Take Count", 0);

        string savedIsCorrectAllWordsPerLevel = PlayerPrefs.GetString("Correctly All Words Per Level", "");
        if(savedIsCorrectAllWordsPerLevel == "")
        {
            isCorrectAllWordsPerLevel = new List<bool>();
            for(int i=0; i<NUMBER_OF_LEVELS + 1; i++)
            {
                if (i == 0) isCorrectAllWordsPerLevel.Add(true);
                else isCorrectAllWordsPerLevel.Add(false);
            }
        }
        else
        {
            isCorrectAllWordsPerLevel = LevelMenuManager.DestringizeIsCorrectAllWordsPerLevel(savedIsCorrectAllWordsPerLevel);
            
        }

        string savedAchievementResult = PlayerPrefs.GetString("Unlocked Achievements", "");
        if (savedAchievementResult == "")
        {
            unlockedAchievements = new Dictionary<Achievement, bool>();
            foreach (Achievement achievement in AchievementManager.instance.achievements)
            {
                unlockedAchievements.Add(achievement, false);
            }
        }
        else
        {
            unlockedAchievements = AchievementManager.DestringizeUnlockedAchievements(savedAchievementResult);
            foreach (Achievement achievement in AchievementManager.instance.achievements)
            {
                achievement.unlocked = unlockedAchievements[achievement];
            }
        }

        string savedCorrectAnsweredWords = PlayerPrefs.GetString("Correctly Answered Words", "");
        if (savedCorrectAnsweredWords == "")
        {
            correctlyAnsweredWords = new Dictionary<Word, bool>();
            foreach (Word word in GameManager.instance.words)
            {
                correctlyAnsweredWords.Add(word, false);
            }
        }
        else
        {
            correctlyAnsweredWords = AchievementManager.DestringizeCorrectlyAnsweredWords(savedCorrectAnsweredWords);
            foreach (Word word in GameManager.instance.words)
            {
                word.answeredCorrectly = correctlyAnsweredWords[word];
            }
        }
    }

    public void SaveAllData()
    {
        PlayerPrefs.SetString("Player Name", playerName);

        //PlayerPrefs.SetString("IsUsingGameElements", isUsingGameElements.ToString());

        foreach (FiveFactorQuestionManager.PlayerType playerType in Enum.GetValues(typeof(FiveFactorQuestionManager.PlayerType)))
        {
            PlayerPrefs.SetInt(playerType + "Percentage", playerTypePercentages[playerType]);
        }

        PlayerPrefs.SetInt("Score Before Learning", scoreBeforeLearning);
        PlayerPrefs.SetInt("Score After Learning", scoreAfterLearning);

        PlayerPrefs.SetInt("Game Phase", (int)gamePhase);
        PlayerPrefs.SetInt("Group", (int)group);

        PlayerPrefs.SetFloat("Play Time", playTime);

        PlayerPrefs.SetInt("BossPlayCnt", bossPlayCnt);
        PlayerPrefs.SetInt("TypPlayCnt",typPlayCnt);
        PlayerPrefs.SetInt("UnlockAchiveCnt", unlockAchiveCnt);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("Exp", exp);

        PlayerPrefs.SetInt("Examination Take Count", examinationTakeCount);

        PlayerPrefs.SetString("Correctly All Words Per Level", LevelMenuManager.StringizeIsCorrectAllWordsPerLevel(isCorrectAllWordsPerLevel));
        PlayerPrefs.SetString("Correctly Answered Words", AchievementManager.StringizeCorrectlyAnsweredWords(correctlyAnsweredWords));
        PlayerPrefs.SetString("Unlocked Achievements", AchievementManager.StringizeUnlockedAchievements(unlockedAchievements));
    }
}
