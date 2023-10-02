using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FiveFactorQuestionManager : MonoBehaviour
{
    //ゲーマータイプ
    public enum PlayerType
    {
        Aesthietic,
        Narrative,
        Goal,
        Social,
        Challenge,
        None
    }

    public static FiveFactorQuestionManager instance
    {
        get;
        private set;
    }

    private List<FiveFactorQuestion> questions;                     //タイプ診断質問のリスト
    private Dictionary<PlayerType, int> answerCounts;               //各タイプの点数をカウント

    private int currentQuestionNumber;                              //現在の質問番号
    
    [field: SerializeField, RenameField("Question Number Text")]
    private TextMeshProUGUI questionNumberText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Question Text")]
    private TextMeshProUGUI questionText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Toggles")]
    private List<Toggle> toggles
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("NextButton")]
    private GameObject nextButton
    {
        get;
        set;
    }

    private void Awake()
    {
        CreateInstance();
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Initialize()
    {
        answerCounts = new Dictionary<PlayerType, int>();
        foreach(PlayerType playerType in Enum.GetValues(typeof(PlayerType)))
        {
            answerCounts.Add(playerType, 0);
        }
        currentQuestionNumber = 0;
        ReadCsvQuestions("Data/FiveFactorQuestionData");
        UpdataQuestion();
    }

    private void ReadCsvQuestions(string filePath)
    {
        questions = new List<FiveFactorQuestion>();
        StringReader stringReader = new StringReader(((TextAsset)Resources.Load(filePath)).text);

        while(stringReader.Peek() != -1)
        {
            bool isReverse = false;
            string[] values = stringReader.ReadLine().Split(',');

            if (values[2] == "R") isReverse = true;
            questions.Add(new FiveFactorQuestion(values[0], GetPlayerTypeFromSymbol(values[1]), isReverse));
            questions = questions.OrderBy(n => Guid.NewGuid()).ToList();
        }
    }

    private PlayerType GetPlayerTypeFromSymbol(string symbol)
    {
        switch (symbol)
        {
            case "A":
                return PlayerType.Aesthietic;
            case "N":
                return PlayerType.Narrative;
            case "G":
                return PlayerType.Goal;
            case "S":
                return PlayerType.Social;
            case "C":
                return PlayerType.Challenge;
        }

        Debug.Log("Invalid player type symbol in CSV file.");
        return PlayerType.None;
    }

    private void UpdataQuestion()
    {
        Debug.Log("Updata");
        if(questions.Count <= currentQuestionNumber)
        {
            return;
        }

        questionNumberText.text = "第" + (currentQuestionNumber + 1) + "問";

        string question = questions[currentQuestionNumber].question;
        questionText.text = question;
        PlayerType playerType = questions[currentQuestionNumber].questionType;
        bool isReverse = questions[currentQuestionNumber].isReverse;

        nextButton.GetComponent<Button>().onClick.RemoveAllListeners();
        nextButton.GetComponent<Button>().onClick.AddListener(() => NextQuestion(playerType, isReverse));
        
    }

    private void NextQuestion(PlayerType type, bool isReverse)
    {
        int point = 0;
        foreach(Toggle t in toggles)
        {
            point++;
            if (t.isOn) break;
        }

        if (isReverse)
        {
            answerCounts[type] += (7 + 1 - point);
        }
        else
        {
            answerCounts[type] += point;
        }
        
        currentQuestionNumber++;

        if(currentQuestionNumber < questions.Count)
        {
            UpdataQuestion();
        }
        else
        {
            Dictionary<PlayerType, int> playerTypePercentages = new Dictionary<PlayerType, int>();
            foreach(PlayerType playerType in Enum.GetValues(typeof(PlayerType)))
            {
                playerTypePercentages.Add(playerType, 0);
            }

            foreach (PlayerType playerType in Enum.GetValues(typeof(PlayerType)))
            {
                playerTypePercentages[playerType] = (int)(answerCounts[playerType] / 35.0f * 100.0f);
            }

            /*
            foreach (PlayerType playerType in Enum.GetValues(typeof(PlayerType)))
            {
                Debug.Log(playerTypePercentages[playerType]);
            }*/

            GameManager.instance.ShowFiveFactorQuestionResultPage();
            FiveFactorQuestionResultManager.instance.ShowResult(playerTypePercentages);
        }
    }
}
