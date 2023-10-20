using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class BossManager : MonoBehaviour
{
    public static BossManager instance
    {
        get;
        private set;
    }

    private const float timeLimit = 10.0f;
    private float countDown;

    private List<Word> examinationWords;
    private int examinationWordsCount;

    private int currentQuestionNumber;

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

    [field: SerializeField, RenameField("TimeText")]
    private TextMeshProUGUI timeText
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

    private void Update()
    {
        if(countDown > 0)
        {
            countDown -= Time.deltaTime;
            timeText.text = "TIME" + " " + countDown.ToString("f1");
        }
        if (countDown < 0)
        {
            countDown = 0;
            timeText.text = "TIME" + " " + countDown.ToString("f1");
            Debug.Log("0秒です");
        }
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
        currentQuestionNumber = 0;
        examinationWordsCount = 0;

        InitializeWords();
        UpdateQuestion();
    }

    private void InitializeWords()
    {
        examinationWords = new List<Word>();

        /// ワードリスト作成 ///

        //リストをランダムに並び変える
        List<Word> randomWords = GameManager.instance.words.OrderBy(n => Guid.NewGuid()).ToList();
        //Level1 or Level2　の単語を6個追加
        for (int i=0; i<GameManager.instance.words.Count; i++)
        {
            if(randomWords[i].level == 1 || randomWords[i].level == 2)
            {
                examinationWords.Add(randomWords[i]);
            }

            if (examinationWords.Count == 6) break;
        }
        //Level3 or Level4　の単語を10個追加
        for(int i=0; i<GameManager.instance.words.Count; i++)
        {
            if(randomWords[i].level == 3 || randomWords[i].level == 4)
            {
                examinationWords.Add(randomWords[i]);
            }

            if (examinationWords.Count == 16) break;
        }
        //Level5　の単語を4個追加
        for(int i=0; i<GameManager.instance.words.Count; i++)
        {
            if (randomWords[i].level == 5)
            {
                examinationWords.Add(randomWords[i]);
            }

            if (examinationWords.Count == 20) break;
        }
        examinationWordsCount = examinationWords.Count;
    }

    public void UpdateQuestion()
    {
        if (examinationWordsCount <= currentQuestionNumber)
        {
            return;
        }

        countDown = timeLimit;

        questionNumberText.text = "第" + (currentQuestionNumber + 1) + "問";

        Word answerWord = examinationWords[currentQuestionNumber];
        int answerWordNumber = UnityEngine.Random.Range(0, 4);
        List<Word> dummyWords = new List<Word>();

        japaneseText.text = answerWord.japanese;

        for (int i = 0; i < wordButtons.Count; i++)
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

        if (currentQuestionNumber < examinationWordsCount)
        {
            UpdateQuestion();
        }
        else
        {
            GameManager.instance.ShowMenuPage();
        }
    }
}
