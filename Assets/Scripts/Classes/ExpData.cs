using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpData
{
    //経験値ディクショナリ
    private Dictionary<int, int> needExpDictionary = new Dictionary<int, int>
    {
        //Lv、経験値
        {2, 15},
        {3, 75},
        {4, 210},
        {5, 330},
        {6, 5000}
    };

    //初めて全問正解した時のボーナス経験値
    private Dictionary<int, int> bonusExpDictionary = new Dictionary<int, int>
    {
        //試験レベル、経験値
        {1, 5},
        {2, 20},
        {3, 45},
        {4, 40},
        {5, 50}
    };

    public int GetNeedForLvUpExp(int level)
    {
        return needExpDictionary[level];
    }

    public int CalcExp(int score, int maximumScore, ExaminationManager.ExaminationLevel level)
    {
        int exp = 0;
        if(score == maximumScore)
        {
            if (!LevelMenuManager.instance.isCorrectLevelsAllWords[(int)(level)])
            {
                LevelMenuManager.instance.isCorrectLevelsAllWords[(int)(level)] = true;
                exp += bonusExpDictionary[(int)level];
                exp += score * (int)level;
            }
            else
            {
                exp += score * (int)level;
            }
            Debug.Log(LevelMenuManager.instance.isCorrectLevelsAllWords[(int)(level)]);
        }
        else
        {
            exp += score * (int)level;
        }

        return exp;
    }
}
