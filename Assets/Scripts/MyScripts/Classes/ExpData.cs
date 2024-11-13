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
        {3, 65},
        {4, 200},
        {5, 300},
        {6, 350},
        {7, 400},
        {8, 450},
        {9, 500},
        {10, 550}
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
            if (!PlayerDataManager.instance.playerData.isCorrectAllWordsPerLevel[(int)(level)])
            {
                PlayerDataManager.instance.playerData.isCorrectAllWordsPerLevel[(int)(level)] = true;
                exp += bonusExpDictionary[(int)level];
                exp += score * (int)level;
            }
            else
            {
                exp += score * (int)level;
            }
            Debug.Log(PlayerDataManager.instance.playerData.isCorrectAllWordsPerLevel[(int)(level)]);
        }
        else
        {
            exp += score * (int)level;
        }

        return exp;
    }
}
