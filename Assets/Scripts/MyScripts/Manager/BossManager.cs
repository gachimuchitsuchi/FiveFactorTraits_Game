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

    private CountDown countDown;

    private int mistakeCount;
    private const int MAX_MISTAKE_COUNT = 2;
    private int life;
    private const int MAX_LIFE = 3;

    private List<Word> examinationWords;
    private int examinationWordsCount;

    private int currentQuestionNumber;

    [field: SerializeField, RenameField("Word Prefab")]
    private GameObject wordPrefab
    {
        get;
        set;
    }

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

    [field: SerializeField, RenameField("LifeText")]
    private TextMeshProUGUI lifeText
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

    [field: SerializeField, RenameField("GameOverPanel")]
    private GameObject gameOverPanel
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

    [field: SerializeField, RenameField("Green Monster Image")]
    private GameObject greenMonsterImage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Purple Monster Image")]
    private GameObject purpleMonsterImage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Red Monster Image")]
    private GameObject redMonsterImage
    {
        get;
        set;
    }

    private int enemyNumber
    {
        get;
        set;
    }

    private char[] alphabet = {'a','b','c','d','e','f','g','h','i','j','k','l',
                               'm','n','o','p','q','r','s','t','u','v','w','x',
                               'y','z'};

    private void Awake()
    {
        CreateInstance();

        countDown = GetComponent<CountDown>();
        SetCountDownEvent();
    }

    private void Start()
    {
        GameObject retryButton = gameOverPanel.transform.Find("RetryButton").gameObject;
        GameObject menuButton = gameOverPanel.transform.Find("MenuButton").gameObject;
        GameObject gameOverText = gameOverPanel.transform.Find("GameOverText").gameObject;

        retryButton.GetComponent<Button>().onClick.AddListener(Retry);
        menuButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowMenuPage);
        gameOverText.SetActive(false);

        enemyNumber = 1;
    }

    private void OnEnable()
    {
        Initialize();
    }

    
    private void Update()
    {
        timeText.text = "TIME" + " " + countDown.GetTime().ToString("f1");
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
        GameObject retryButton = gameOverPanel.transform.Find("RetryButton").gameObject;
        GameObject gameOverText = gameOverPanel.transform.Find("GameOverText").gameObject;
        retryButton.SetActive(true);
        gameOverText.SetActive(false);

        currentQuestionNumber = 0;
        examinationWordsCount = 0;
        life = MAX_LIFE;
        lifeText.text = "×" + life;

        purpleMonsterImage.SetActive(false);
        greenMonsterImage.SetActive(false);
        redMonsterImage.SetActive(false);

        gameOverPanel.SetActive(false);
        

        InitializeWords();
        StartCoroutine(UpdateQuestion());
    }

    private void SetCountDownEvent()
    {
        countDown.countZeroEvent += PlayGameOverCoroutine;
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

    public IEnumerator UpdateQuestion()
    {
        if (examinationWordsCount <= currentQuestionNumber)
        {
            yield break;
        }

        enemyNumber = UnityEngine.Random.Range(1, 4);
        Debug.Log(enemyNumber + "teki");

        japaneseText.text = "";

        yield return StartCoroutine(AppearEnemy(enemyNumber));
        gameObject.GetComponent<ShakeByRandom>().StartShake(0.3f, 0.3f, 0.3f);

        lifeText.text = "×" + (life-1);
        countDown.StartCountDown();
        japaneseText.GetComponent<ChangeUISize>().StartEnlarge();
        mistakeCount = 0;

        questionNumberText.text = "第" + (currentQuestionNumber + 1) + "問";

        Word answerWord = examinationWords[currentQuestionNumber];
        int answerWordNumber = UnityEngine.Random.Range(0, 4);
        List<Word> dummyWords = new List<Word>();
        List<char> dummySpells = new List<char>();

        if(enemyNumber == 1)
        {
            japaneseText.text = answerWord.japanese;

            for (int i = 0; i < wordButtons.Count; i++)
            {
                int index = i;
                wordButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                ColorBlock colorblock = wordButtons[i].GetComponent<Button>().colors;

                if (i == answerWordNumber)
                {
                    wordButtons[i].GetComponent<WordButtonBehaviour>().isAnswer = true;
                    wordButtons[i].GetComponent<WordButtonBehaviour>().word = answerWord;
                    colorblock.disabledColor = Color.green;
                    wordButtons[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(ShowAnswer(true, wordButtons[index])));
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
                    colorblock.disabledColor = Color.red;
                    wordButtons[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(ShowAnswer(false, wordButtons[index])));
                }
                wordButtons[i].GetComponent<WordButtonBehaviour>().UpdateEnglishWord();
                wordButtons[i].GetComponent<Button>().colors = colorblock;
            }
        }

        if(enemyNumber == 2)
        {
            japaneseText.text = answerWord.english;

            for (int i = 0; i < wordButtons.Count; i++)
            {
                int index = i;
                wordButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                ColorBlock colorblock = wordButtons[i].GetComponent<Button>().colors;

                if (i == answerWordNumber)
                {
                    wordButtons[i].GetComponent<WordButtonBehaviour>().isAnswer = true;
                    wordButtons[i].GetComponent<WordButtonBehaviour>().word = answerWord;
                    colorblock.disabledColor = Color.green;
                    wordButtons[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(ShowAnswer(true, wordButtons[index])));
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
                    colorblock.disabledColor = Color.red;
                    wordButtons[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(ShowAnswer(false, wordButtons[index])));
                }
                wordButtons[i].GetComponent<WordButtonBehaviour>().UpdateJapaneseWord();
                wordButtons[i].GetComponent<Button>().colors = colorblock;
            }
        }

        if(enemyNumber == 3)
        {
            string questionText = answerWord.english;
            int length = questionText.Length;
            int rndNum = UnityEngine.Random.Range(0,length);
            char answerSpell = questionText[rndNum];
            questionText = questionText.Replace(answerSpell, '□');

            japaneseText.text = questionText;

            for (int i = 0; i < wordButtons.Count; i++)
            {
                int index = i;
                wordButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                ColorBlock colorblock = wordButtons[i].GetComponent<Button>().colors;

                if (i == answerWordNumber)
                {
                    wordButtons[i].GetComponent<WordButtonBehaviour>().isAnswer = true;
                    wordButtons[i].GetComponent<WordButtonBehaviour>().word = answerWord;
                    wordButtons[i].GetComponent<WordButtonBehaviour>().spell = answerSpell;
                    colorblock.disabledColor = Color.green;
                    wordButtons[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(ShowAnswer(true, wordButtons[index])));
                }
                else
                {
                    wordButtons[i].GetComponent<WordButtonBehaviour>().isAnswer = false;

                    char dummySpell;
                    do
                    {
                        dummySpell = alphabet[UnityEngine.Random.Range(0, 26)];
                    }
                    while (dummySpells.Contains(dummySpell) || dummySpell == answerSpell);
                    dummySpells.Add(dummySpell);

                    wordButtons[i].GetComponent<WordButtonBehaviour>().spell = dummySpell;
                    colorblock.disabledColor = Color.red;
                    wordButtons[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine(ShowAnswer(false, wordButtons[index])));
                }
                wordButtons[i].GetComponent<WordButtonBehaviour>().UpdateSpell();
                wordButtons[i].GetComponent<Button>().colors = colorblock;
            }
        }
        
    }

    private IEnumerator AppearEnemy(int num)
    {
        if(num == 1)
        {
            purpleMonsterImage.SetActive(true);

            yield return new WaitForSeconds(1f);

            purpleMonsterImage.SetActive(false);
        }

        if(num == 2)
        {
            greenMonsterImage.SetActive(true);

            yield return new WaitForSeconds(1f);

            greenMonsterImage.SetActive(false);
        }

        if(num == 3)
        {
            redMonsterImage.SetActive(true);

            yield return new WaitForSeconds(1f);

            redMonsterImage.SetActive(false);
        }
        
    }

    private IEnumerator ShowAnswer(bool answeredCorrectly, GameObject pushWordButton)
    {
        if (answeredCorrectly)
        {
            circleSignImage.SetActive(true);
            japaneseText.GetComponent<ChangeUISize>().StopEnlarge();
            countDown.StopCountDown();
        }
        else
        {
            mistakeCount++;
            pushWordButton.GetComponent<Image>().color = Color.red;
            pushWordButton.GetComponent<Button>().interactable = false;
            if (mistakeCount < MAX_MISTAKE_COUNT)
            {
                yield break;
            }else if(mistakeCount == MAX_MISTAKE_COUNT)
            {
                PlayGameOverCoroutine();
                yield break;
            }
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

        foreach (GameObject wordButton in wordButtons)
        {
            wordButton.GetComponent<Image>().color = Color.white;
            wordButton.GetComponent<Button>().interactable = true;
        }

        currentQuestionNumber++;

        if (currentQuestionNumber < examinationWordsCount)
        {
            StartCoroutine(UpdateQuestion());
        }
        else
        {
            GameManager.instance.ShowMenuPage();
        }
    }

    private void Retry()
    {
        currentQuestionNumber++;
        gameOverPanel.GetComponent<AnimatedDialog>().Close();
        StartCoroutine(UpdateQuestion());
    }

    private void PlayGameOverCoroutine()
    {
        StartCoroutine("GameOver");
    }

    private IEnumerator GameOver()
    {
        life--;
        japaneseText.GetComponent<ChangeUISize>().StopEnlarge();
        countDown.StopCountDown();

        foreach (GameObject wordButton in wordButtons)
        {
            wordButton.GetComponent<Button>().interactable = false;
        }

        gameObject.GetComponent<ShakeByRandom>().StartShake(1.0f, 1.0f, 1.0f);

        yield return new WaitForSeconds(1.5f);

        gameOverPanel.GetComponent<AnimatedDialog>().Open();

        //ワードボードをゲームオーバーパネルの子階層に作成
        GameObject wordElement = Instantiate(wordPrefab, gameOverPanel.transform);


        //Retry時、次の問題にいくため問題更新
        for(int i=0; i<wordButtons.Count; i++)
        {
            if (wordButtons[i].GetComponent<WordButtonBehaviour>().isAnswer)
            {
                wordElement.GetComponent<WordBehaviour>().word = wordButtons[i].GetComponent<WordButtonBehaviour>().word;
                wordElement.GetComponent<WordBehaviour>().UpdateWord();
            }
        }

        if (life == 0) 
        {
            GameObject retryButton = gameOverPanel.transform.Find("RetryButton").gameObject;
            GameObject gameOverText = gameOverPanel.transform.Find("GameOverText").gameObject;

            retryButton.SetActive(false);
            gameOverText.SetActive(true);
        }

        foreach (GameObject wordButton in wordButtons)
        {
            wordButton.GetComponent<Image>().color = Color.white;
            wordButton.GetComponent<Button>().interactable = true;
        }
    }
}
