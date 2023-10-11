using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpData
{
    //経験値ディクショナリ
    private Dictionary<int, int> needExpDictionary = new Dictionary<int, int>
    {
        //Lv、経験値
        {2, 20},
        {3, 50},
        {4, 100},
        {5, 150},
        {6, 200}
    };

    public int GetNeedForLvUpExp(int level)
    {
        return needExpDictionary[level];
    }
}
