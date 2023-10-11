using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpData
{
    //�o���l�f�B�N�V���i��
    private Dictionary<int, int> needExpDictionary = new Dictionary<int, int>
    {
        //Lv�A�o���l
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
