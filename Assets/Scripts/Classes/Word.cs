using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word
{
    public string english
    {
        get;
        set;
    }

    public string japanese
    {
        get;
        set;
    }

    public int level
    {
        get;
        set;
    }

    public Word(string english, string japanese, int level)
    {
        this.english = english;
        this.japanese = japanese;
        this.level = level;
    }
}
