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

        playerTypePercentages = new Dictionary<FiveFactorQuestionManager.PlayerType, int>();
        foreach(FiveFactorQuestionManager.PlayerType playerType in Enum.GetValues(typeof(FiveFactorQuestionManager.PlayerType)))
        {
            playerTypePercentages.Add(playerType, PlayerPrefs.GetInt(playerType + "Percentage", 0));
        }

        gamePhase = (GamePhase)PlayerPrefs.GetInt("Game Phase", (int)GamePhase.FirstExamination);

        level = PlayerPrefs.GetInt("Level", 1);
        exp = PlayerPrefs.GetInt("Exp", 0);

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
    }

    public void SaveAllData()
    {
        PlayerPrefs.SetString("Player Name", playerName);

        foreach (FiveFactorQuestionManager.PlayerType playerType in Enum.GetValues(typeof(FiveFactorQuestionManager.PlayerType)))
        {
            PlayerPrefs.SetInt(playerType + "Percentage", playerTypePercentages[playerType]);
        }

        PlayerPrefs.SetInt("Game Phase", (int)gamePhase);

        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("Exp", exp);

        PlayerPrefs.SetString("Correctly All Words Per Level", LevelMenuManager.StringizeIsCorrectAllWordsPerLevel(isCorrectAllWordsPerLevel));

        PlayerPrefs.SetString("Correctly Answered Words", AchievementManager.StringizeCorrectlyAnsweredWords(correctlyAnsweredWords));
        PlayerPrefs.SetString("Unlocked Achievements", AchievementManager.StringizeUnlockedAchievements(unlockedAchievements));
    }
}
