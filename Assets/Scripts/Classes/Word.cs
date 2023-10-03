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

    public Word(string english, string japanese)
    {
        this.english = english;
        this.japanese = japanese;
    }
}
