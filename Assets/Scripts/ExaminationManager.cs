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

    private List<Word> examinationWords;
    private int examinationWordsCount;

    private int currentQuestionNumber;

    private int score;

    [field: SerializeField, RenameField("Queston Number Text")]
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

    private void Initialize()
    {
        score = 0;
        currentQuestionNumber = 0;
        examinationWordsCount = GameManager.instance.words.Count;

        InitializeWords();
        UpdateQuestion();
    }

    private void InitializeWords()
    {
        examinationWords = new List<Word>();
        //ƒŠƒXƒg‚ðƒ‰ƒ“ƒ_ƒ€‚É•À‚Ñ•Ï‚¦‚é
        List<Word> randomWords = GameManager.instance.words.OrderBy(n => Guid.NewGuid()).ToList();
        for(int i=0; i<examinationWordsCount; i++)
        {
            examinationWords.Add(randomWords[i]);
        }
    }

    public void UpdateQuestion()
    {
        if(examinationWordsCount <= currentQuestionNumber)
        {
            return;
        }

        questionNumberText.text = "‘æ" + (currentQuestionNumber + 1) + "–â";

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
    }

    private IEnumerator ShowAnswer(bool answeredCorrectly)
    {
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

        yield return new WaitForSeconds(2f);

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
            ExaminationResultManager.instance.ShowResult(score, examinationWordsCount);
            GameManager.instance.ShowExaminationResultPage();
        }
    }
}
