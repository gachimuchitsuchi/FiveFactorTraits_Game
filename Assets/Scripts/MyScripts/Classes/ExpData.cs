using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpData
{
    //�o���l�f�B�N�V���i��
    private Dictionary<int, int> needExpDictionary = new Dictionary<int, int>
    {
        //Lv�A�o���l
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

    //���߂đS�␳���������̃{�[�i�X�o���l
    private Dictionary<int, int> bonusExpDictionary = new Dictionary<int, int>
    {
        //�������x���A�o���l
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
