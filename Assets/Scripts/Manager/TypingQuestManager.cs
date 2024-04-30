using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TypingQuestManager : MonoBehaviour
{
    public static TypingQuestManager instance
    {
        get;
        private set;
    }

    private CountDown countDown;

    private List<Word> examinationWords;
    private int examinationWordsCount;

    private int currentQuestionNumber;

    private bool beingMeasured;
    private float startTime;
    private float timeScore;

    private  List<char> spell = new List<char>();

    private int spellIndex;

    [field: SerializeField, RenameField("Question Number Text")]
    private TextMeshProUGUI questionNumberText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("JapaneseText")] 
    private TextMeshProUGUI japaneseText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("SpellText")] 
    private TextMeshProUGUI spellText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("CountDownText")]
    private GameObject countDownText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("StartPanel")]
    private GameObject startPanel
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("QuestionCountToggles")]
    private List<Toggle> questionCountToggles
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("WarningText")]
    private TextMeshProUGUI warningText
    {
        get;
        set;
    }

    private void Start()
    {
        countDown = GetComponent<CountDown>();
        SetCountDownZeroEvent();
    }

    private void Update()
    {
        //カウントダウン切り上げ表記
        if(!beingMeasured) countDownText.GetComponent<TextMeshProUGUI>().text = Math.Ceiling(countDown.GetTime()).ToString("f0");
    }

    private void OnEnable()
    {
        Initialize();
        InitializeWords();
        InitializeQuestion();
    }


    private void OnGUI()
    {
        if (!beingMeasured)
        {
            return;
        }

        if(Event.current.type == EventType.KeyDown)
        {
            switch (InputKey(GetCharFromKeyCode(Event.current.keyCode)))
            {
                case 1: // 正解タイプ時
                    spellIndex++;
                    if (spell[spellIndex] == '@') // 「@」がタイピングの終わりの判定となる。
                    {
                        currentQuestionNumber++;
                        if (currentQuestionNumber < examinationWordsCount)
                        {
                            UpdateQuestion();
                        }
                        else
                        {
                            timeScore = Time.time - startTime;
                            TypingQuestResultManager.instance.ShowResult(timeScore, examinationWordsCount);
                            GameManager.instance.ShowTypingQuestResultPage();
                        }
                    }
                    else
                    {
                        spellText.text = GenerateSpellText();
                    }
                    break;
                case 2: // ミスタイプ時
                    break;
            }
        }        
    }

    private void Initialize()
    {
        startPanel.SetActive(true);
        countDownText.SetActive(false);
        warningText.text = "";
        foreach (Toggle t in questionCountToggles) t.isOn = false;


        beingMeasured = false;
    }

    private void InitializeWords()
    {
        examinationWords = new List<Word>();
        //ワードリストをランダムに並び変える
        List<Word> randomWords = GameManager.instance.words.OrderBy(n => Guid.NewGuid()).ToList();

        //試験リスト作成
        for (int i = 0; i < randomWords.Count; i++)
        {
            examinationWords.Add(randomWords[i]);
        }

        //examinationWordsCount = examinationWords.Count;
        //Debug.Log(examinationWordsCount);
    }

    void InitializeQuestion()
    {
        currentQuestionNumber = 0;
        Word question = examinationWords[currentQuestionNumber];

        spell.Clear();

        spellIndex = 0;

        char[] characters = question.english.ToCharArray();

        foreach (char character in characters)
        {
            spell.Add(character);
        }

        spell.Add('@');

        japaneseText.text = question.japanese;
        spellText.text = GenerateSpellText();
        questionNumberText.text = "第" + (currentQuestionNumber + 1) + "問";
    }

    void UpdateQuestion()
    {
        if (examinationWordsCount <= currentQuestionNumber)
        {
            return;
        }

        Word question = examinationWords[currentQuestionNumber];

        spell.Clear();

        spellIndex = 0;

        char[] characters = question.english.ToCharArray();

        foreach (char character in characters)
        {
            spell.Add(character);
        }

        spell.Add('@');

        japaneseText.text = question.japanese;
        spellText.text = GenerateSpellText();
        questionNumberText.text = "第" + (currentQuestionNumber + 1) + "問";
    }

    public void GameStart()
    {
        if (!CheckToggles())
        {
            warningText.text = "問題数を選んでください";
            return;
        }
        countDown.StartCountDown();
        startPanel.SetActive(false);
        countDownText.SetActive(true);
        foreach (Toggle t in questionCountToggles) t.isOn = false;
    }

    public void ChangeColorToggle()
    {
        foreach(Toggle t in questionCountToggles)
        {
            if (t.isOn)
            {
                var newColors = t.colors;
                newColors.normalColor = Color.red;
                newColors.highlightedColor = Color.red;
                newColors.selectedColor = Color.red;
                t.colors = newColors;
            }
            else
            {
                var newColors = t.colors;
                newColors.normalColor = Color.white;
                newColors.highlightedColor = Color.white;
                newColors.selectedColor = Color.white;
                t.colors = newColors;
            }
        }
    }

    private bool CheckToggles()
    {
        int i = 1;
        foreach(Toggle t in questionCountToggles)
        {
            if (t.isOn)
            {
                examinationWordsCount = i * 10;
                return true;
            }
            else i++;
        }

        return false;
    }

    private void SetCountDownZeroEvent()
    {
        countDown.countZeroEvent += TypingStart;
    }

    private void TypingStart()
    {
        beingMeasured = true;
        startTime = Time.time;
        countDown.StopCountDown();
        countDownText.SetActive(false);
    }

    string GenerateSpellText ()
    {
        string text = "<style=typed>";
        for (int i = 0; i < spell.Count; i++)
        {
            if (spell[i] == '@')
            {
                break;
            }

            if (i == spellIndex)
            {
                text += "</style><style=untyped>";
            }

            text += spell[i];
        }

        text += "</style>";
        //Debug.Log(text);
        return text;
    }

    int InputKey(char inputChar)
    {
        if (inputChar == '\0')
        {
            return 0;
        }

        if (inputChar == spell[spellIndex])
        {
            return 1;
        }

        return 2;
    }

    char GetCharFromKeyCode(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.A:
                return 'a';
            case KeyCode.B:
                return 'b';
            case KeyCode.C:
                return 'c';
            case KeyCode.D:
                return 'd';
            case KeyCode.E:
                return 'e';
            case KeyCode.F:
                return 'f';
            case KeyCode.G:
                return 'g';
            case KeyCode.H:
                return 'h';
            case KeyCode.I:
                return 'i';
            case KeyCode.J:
                return 'j';
            case KeyCode.K:
                return 'k';
            case KeyCode.L:
                return 'l';
            case KeyCode.M:
                return 'm';
            case KeyCode.N:
                return 'n';
            case KeyCode.O:
                return 'o';
            case KeyCode.P:
                return 'p';
            case KeyCode.Q:
                return 'q';
            case KeyCode.R:
                return 'r';
            case KeyCode.S:
                return 's';
            case KeyCode.T:
                return 't';
            case KeyCode.U:
                return 'u';
            case KeyCode.V:
                return 'v';
            case KeyCode.W:
                return 'w';
            case KeyCode.X:
                return 'x';
            case KeyCode.Y:
                return 'y';
            case KeyCode.Z:
                return 'z';
            case KeyCode.Alpha0:
                return '0';
            case KeyCode.Alpha1:
                return '1';
            case KeyCode.Alpha2:
                return '2';
            case KeyCode.Alpha3:
                return '3';
            case KeyCode.Alpha4:
                return '4';
            case KeyCode.Alpha5:
                return '5';
            case KeyCode.Alpha6:
                return '6';
            case KeyCode.Alpha7:
                return '7';
            case KeyCode.Alpha8:
                return '8';
            case KeyCode.Alpha9:
                return '9';
            case KeyCode.Minus:
                return '-';
            case KeyCode.Caret:
                return '^';
            case KeyCode.Backslash:
                return '\\';
            case KeyCode.At:
                return '@';
            case KeyCode.LeftBracket:
                return '[';
            case KeyCode.Semicolon:
                return ';';
            case KeyCode.Colon:
                return ':';
            case KeyCode.RightBracket:
                return ']';
            case KeyCode.Comma:
                return ',';
            case KeyCode.Period:
                return '.';
            case KeyCode.Slash:
                return '/';
            case KeyCode.Underscore:
                return '_';
            case KeyCode.Backspace:
                return '\b';
            case KeyCode.Return:
                return '\r';
            case KeyCode.Space:
                return ' ';
            default: //上記以外のキーが押された場合は「null文字」を返す。
                return '\0';
        }
    }
}
