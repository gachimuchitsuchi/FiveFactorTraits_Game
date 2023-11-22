using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class ExaminationManager : MonoBehaviour
{
    public static ExaminationManager instance
    {
        get;
        private set;
    }

    public enum ExaminationLevel
    {
        All,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5
    }

    private ExaminationLevel examinationLevel
    {
        get;
        set;
    }

    private List<Word> examinationWords;
    private int examinationWordsCount;

    private int currentQuestionNumber;

    private int score;

    [field: SerializeField, RenameField("Question Number Text")]
    private TextMeshProUGUI questionNumberText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Japanese Text")]
    private TextMeshProUGUI japaneseText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Word Buttons")]
    private List<GameObject> wordButtons
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Skip Button")]
    private GameObject skipButton
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("DescriptionPanel")]
    private GameObject descriptionPanel
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Circle Sign Image")]
    private GameObject circleSignImage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Cross Sign Image")]
    private GameObject crossSignImage
    {
        get;
        set;
    }

    private void Awake()
    {
        CreateInstance();
    }

    private void Start()
    {
        GameObject descriptionButton = descriptionPanel.transform.Find("StartButton").gameObject;

        descriptionButton.GetComponent<Button>().onClick.AddListener(CloseDescriptionPanel);
        skipButton.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(ShowAnswer(false)));
    }

    private void OnEnable()
    {
        Initialize();

        OpenDescriptionPanel();
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Initialize()
    {
        score = 0;
        currentQuestionNumber = 0;
        examinationWordsCount = 0;

        if(PlayerDataManager.instance.playerData.gamePhase == PlayerData.GamePhase.FirstExamination)
        {
            skipButton.SetActive(true);
        }
        else
        {
            skipButton.SetActive(false);
        }

        InitializeWords();
        UpdateQuestion();
    }

    private void InitializeWords()
    {
        examinationWords = new List<Word>();
        //ワードリストをランダムに並び変える
        List<Word> randomWords = GameManager.instance.words.OrderBy(n => Guid.NewGuid()).ToList();
        Debug.Log(examinationLevel);

        //試験リスト作成
        if(examinationLevel == ExaminationLevel.All)
        {
            for (int i = 0; i < randomWords.Count; i++)
            {
                examinationWords.Add(randomWords[i]);
            }
        }
        else
        {
            for (int i = 0; i < randomWords.Count; i++)
            {
                if(randomWords[i].level == (int)examinationLevel)
                {
                    examinationWords.Add(randomWords[i]);
                }
            }
        }
        examinationWordsCount = examinationWords.Count;
    }

    public void UpdateQuestion()
    {
        if(examinationWordsCount <= currentQuestionNumber)
        {
            return;
        }

        questionNumberText.text = "第" + (currentQuestionNumber + 1) + "問";

        Word answerWord = examinationWords[currentQuestionNumber];
        int answerWordNumber = UnityEngine.Random.Range(0, 4);
        List<Word> dummyWords = new List<Word>();

        japaneseText.text = answerWord.japanese;

        for(int i=0; i<wordButtons.Count; i++)
        {
            wordButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();

            if (i == answerWordNumber)
            {
                wordButtons[i].GetComponent<WordButtonBehaviour>().isAnswer = true;
                wordButtons[i].GetComponent<WordButtonBehaviour>().word = answerWord;
                wordButtons[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(ShowAnswer(true)));
                wordButtons[i].GetComponent<Button>().onClick.AddListener(() => AnswerCorrectWord(answerWord));
            }
            else
            {
                wordButtons[i].GetComponent<WordButtonBehaviour>().isAnswer = false;

                Word dummyWord;
                do
                {
                    dummyWord = GameManager.instance.words[UnityEngine.Random.Range(0, GameManager.instance.words.Count)];
                }
                while (dummyWords.Contains(dummyWord) || dummyWord == answerWord);
                dummyWords.Add(dummyWord);

                wordButtons[i].GetComponent<WordButtonBehaviour>().word = dummyWord;
                wordButtons[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(ShowAnswer(false)));
            }
            wordButtons[i].GetComponent<WordButtonBehaviour>().UpdateWord();
        }

        skipButton.GetComponent<Button>().interactable = true;
    }

    private void AnswerCorrectWord(Word answeredWord)
    {
        GameManager.instance.words.FirstOrDefault(n => n == answeredWord).answeredCorrectly = true;
        PlayerDataManager.instance.playerData.correctlyAnsweredWords[answeredWord] = true;

        if (15 <= GameManager.instance.words.Count(n => n.answeredCorrectly == true))
        {
            AchievementManager.instance.UnlockAchievement(4);
        }
        if (35 <= GameManager.instance.words.Count(n => n.answeredCorrectly == true))
        {
            AchievementManager.instance.UnlockAchievement(5);
        }
        if (50 <= GameManager.instance.words.Count(n => n.answeredCorrectly == true))
        {
            AchievementManager.instance.UnlockAchievement(6);
        }
    }

    private IEnumerator ShowAnswer(bool answeredCorrectly)
    {
        skipButton.GetComponent<Button>().interactable = false;

        if (answeredCorrectly)
        {
            score++;

            circleSignImage.SetActive(true);
        }
        else
        {
            crossSignImage.SetActive(true);
        }

        foreach (GameObject wordButton in wordButtons)
        {
            if (wordButton.GetComponent<WordButtonBehaviour>().isAnswer)
            {
                wordButton.GetComponent<Image>().color = Color.green;
            }
            else
            {
                wordButton.GetComponent<Image>().color = Color.red;
            }
        }

        yield return new WaitForSeconds(1f);

        circleSignImage.SetActive(false);
        crossSignImage.SetActive(false);

        foreach (GameObject wordButton in wordButtons)
        {
            wordButton.GetComponent<Image>().color = Color.white;
        }

        currentQuestionNumber++;

        if(currentQuestionNumber < examinationWordsCount)
        {
            UpdateQuestion();
        }
        else
        {
            ExaminationResultManager.instance.ShowResult(score, examinationWordsCount, examinationLevel);
            GameManager.instance.ShowExaminationResultPage(examinationLevel);
        }
    }

    public void SetExaminationLevel(ExaminationLevel level)
    {
        //Debug.Log(level);
        examinationLevel = level;
    }

    private void OpenDescriptionPanel()
    {
        if(PlayerDataManager.instance.playerData.gamePhase == PlayerData.GamePhase.FirstExamination)
        {
            descriptionPanel.SetActive(true);
            descriptionPanel.GetComponent<AnimatedDialog>().SetIsFirstExamination(true);
            descriptionPanel.GetComponent<AnimatedDialog>().Open();
        }
        else
        {
            descriptionPanel.SetActive(false);
            descriptionPanel.GetComponent<AnimatedDialog>().SetIsFirstExamination(false);
        }
    }

    private void CloseDescriptionPanel()
    {
        descriptionPanel.GetComponent<AnimatedDialog>().Close();
    }
}
