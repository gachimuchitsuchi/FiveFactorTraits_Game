using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiveFactorQuestion
{
    public string question
    {
        get;
        private set;
    }

    public FiveFactorQuestionManager.PlayerType questionType
    {
        get;
        private set;
    }

    public bool isReverse
    {
        get;
        private set;
    }

    public FiveFactorQuestion(string question, FiveFactorQuestionManager.PlayerType questionType, bool isReverse)
    {
        this.question = question;
        this.questionType = questionType;
        this.isReverse = isReverse;
    }
}
