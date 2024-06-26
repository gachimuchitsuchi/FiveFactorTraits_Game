using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get;
        private set;
    }

    public List<Word> words
    {
        get;
        private set;
    }

    private const string wordsFilePath = "Data/words";

    public GameObject currentPage
    {
        get;
        private set;
    }

    [field: SerializeField, RenameField("TitlePage")]
    private GameObject titlePage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("PlayerDataPage")]
    private GameObject playerDataPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("StartPage")]
    private GameObject startPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("FiveFactorQuestionPage")]
    private GameObject ffqPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("FiveFactorQuestionResultPage")]
    private GameObject ffqResultPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("MenuPage")]
    private GameObject menuPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("ExaminationPage")]
    private GameObject examinationPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("ExaminationResultPage")]
    private GameObject examinationResultPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("ModeSelectionPage")]
    private GameObject modeSelectionPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("WordListPage")]
    private GameObject wordListPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("GameModeMenuPage")]
    private GameObject gameModeMenuPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("ExaminationMenuPage")]
    private GameObject examinationMenuPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("LevelMenuPage")]
    private GameObject levelMenuPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("BossMenuPage")]
    private GameObject bossMenuPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("BossPage")]
    private GameObject bossPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("AchievementPage")]
    private GameObject achievementPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("QuestMenuPage")]
    private GameObject questMenuPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("TypingQuestPage")]
    private GameObject typingQuestPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("TypingQuestResultPage")]
    private GameObject typingQuestResultPage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("LeftButton")]
    private GameObject leftButton
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("RightButton")]
    private GameObject rightButton
    {
        get;
        set;
    }

    private void Awake()
    {
        CreateInstance();
        Initialize();
        SetAllPagesActive(true);
    }

    private void Start()
    {
        SetAllPagesActive(false);
        ShowTitlePage();
    }

    private void OnApplicationQuit()
    {
        PlayerDataManager.instance.playerData.SaveAllData();
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
        ReadCsvWords();
    }

    private void ReadCsvWords()
    {
        words = new List<Word>();
        //csvファイル読み取り
        StringReader stringReader = new StringReader(((TextAsset)Resources.Load(wordsFilePath)).text);

        //ストリームの末端まで繰り返す
        while(stringReader.Peek() != -1)
        {
            string[] values = stringReader.ReadLine().Split(',');
            words.Add(new Word(values[0], values[1], int.Parse(values[2])));
        }
        //アルファベット順にソート
        words.Sort((a, b) => a.english.CompareTo(b.english));
    }

    private void SetAllPagesActive(bool active)
    {
        achievementPage.SetActive(active);
        //titlePage.SetActive(active);
        playerDataPage.SetActive(active);
        startPage.SetActive(active);
        ffqPage.SetActive(active);
        ffqResultPage.SetActive(active);
        menuPage.SetActive(active);
        examinationPage.SetActive(active);
        examinationResultPage.SetActive(active);
        questMenuPage.SetActive(active);
        typingQuestPage.SetActive(active);
        typingQuestResultPage.SetActive(active);
        modeSelectionPage.SetActive(active);
        wordListPage.SetActive(active);
        gameModeMenuPage.SetActive(active);
        examinationMenuPage.SetActive(active);
        levelMenuPage.SetActive(active);
        bossMenuPage.SetActive(active);
        bossPage.SetActive(active);
    }

    public void Play()
    {
        switch (PlayerDataManager.instance.playerData.gamePhase)
        {
            case PlayerData.GamePhase.FirstExamination:
                if(PlayerDataManager.instance.playerData.playerName == "")
                {
                    ShowStartPage();
                }
                else
                {
                    ShowExaminationPage(ExaminationManager.ExaminationLevel.All);
                }
                break;
            case PlayerData.GamePhase.FiveFactorQuestion:
                ShowFiveFactorQuestionPage();
                break;
            case PlayerData.GamePhase.Learning:
                ShowMenuPage();
                break;
            case PlayerData.GamePhase.FinalExamination:
                ShowExaminationPage(ExaminationManager.ExaminationLevel.All);
                break;
            case PlayerData.GamePhase.Completed:
                break;
        }
    }

    public void ShowTitlePage()
    {
        currentPage?.SetActive(false);
        currentPage = titlePage;
        currentPage?.SetActive(true);

        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    public void ShowPlayerDataPage()
    {
        currentPage?.SetActive(false);
        currentPage = playerDataPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowTitlePage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "BACK";

        rightButton.SetActive(false);
    }

    public void ShowStartPage()
    {
        currentPage?.SetActive(false);
        currentPage = startPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowTitlePage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "BACK";

        rightButton.SetActive(true);
        rightButton.GetComponent<Button>().onClick.RemoveAllListeners();
        rightButton.GetComponent<Button>().onClick.AddListener(StartManager.instance.SavePlayerName);
        rightButton.GetComponentInChildren<TextMeshProUGUI>().text = "OK";

    }

    public void ShowFiveFactorQuestionPage()
    {
        currentPage?.SetActive(false);
        currentPage = ffqPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    public void ShowFiveFactorQuestionResultPage()
    {
        currentPage?.SetActive(false);
        currentPage = ffqResultPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(false);

        rightButton.SetActive(true);
        rightButton.GetComponent<Button>().onClick.RemoveAllListeners();
        rightButton.GetComponent<Button>().onClick.AddListener(ShowModeSelectionPage);
        rightButton.GetComponentInChildren<TextMeshProUGUI>().text = "NEXT";
    }

    public void ShowExaminationPage(ExaminationManager.ExaminationLevel level)
    {
        ExaminationManager.instance.SetExaminationLevel(level);
        currentPage?.SetActive(false);
        currentPage = examinationPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(false);

        rightButton.SetActive(false);
    }

    public void ShowExaminationResultPage(ExaminationManager.ExaminationLevel level)
    {
        currentPage?.SetActive(false);
        currentPage = examinationResultPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(false);

        //Debug.Log(PlayerDataManager.instance.playerData.gamePhase);

        switch (PlayerDataManager.instance.playerData.gamePhase)
        {
            case PlayerData.GamePhase.FiveFactorQuestion:
                rightButton.SetActive(true);
                rightButton.GetComponent<Button>().onClick.RemoveAllListeners();
                rightButton.GetComponent<Button>().onClick.AddListener(ShowFiveFactorQuestionPage);
                rightButton.GetComponentInChildren<TextMeshProUGUI>().text = "NEXT";
                break;

            case PlayerData.GamePhase.Learning:
                rightButton.SetActive(true);
                rightButton.GetComponent<Button>().onClick.RemoveAllListeners();
                if(level == ExaminationManager.ExaminationLevel.All)
                {
                    rightButton.GetComponent<Button>().onClick.AddListener(ShowExaminationMenuPage);
                }
                else
                {
                    rightButton.GetComponent<Button>().onClick.AddListener(ShowLevelMenuPage);
                }
                rightButton.GetComponentInChildren<TextMeshProUGUI>().text = "OK";
                break;

            case PlayerData.GamePhase.Completed:
                rightButton.SetActive(true);
                rightButton.GetComponent<Button>().onClick.RemoveAllListeners();
                rightButton.GetComponent<Button>().onClick.AddListener(ShowTitlePage);
                rightButton.GetComponentInChildren<TextMeshProUGUI>().text = "OK";
                break;
        }
        
    }

    public void ShowModeSelectionPage()
    {
        currentPage?.SetActive(false);
        currentPage = modeSelectionPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    public void ShowMenuPage()
    {
        currentPage?.SetActive(false);
        currentPage = menuPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowTitlePage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "TITLE";

        rightButton.SetActive(false);
    }

    public void ShowWordListPage()
    {
        currentPage?.SetActive(false);
        currentPage = wordListPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowMenuPage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "BACK";

        rightButton.SetActive(false);
    }

    public void ShowGameModeMenuPage()
    {
        currentPage?.SetActive(false);
        currentPage = gameModeMenuPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowMenuPage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "BACK";

        rightButton.SetActive(false);
    }

    public void ShowExaminationMenuPage()
    {
        currentPage?.SetActive(false);
        currentPage = examinationMenuPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowMenuPage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "BACK";

        rightButton.SetActive(false);
    }

    public void ShowLevelMenuPage()
    {
        currentPage?.SetActive(false);
        currentPage = levelMenuPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowGameModeMenuPage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "BACK";

        rightButton.SetActive(false);
    }

    public void ShowBossMenuPage()
    {
        currentPage?.SetActive(false);
        currentPage = bossMenuPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowGameModeMenuPage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "BACK";

        rightButton.SetActive(false);
    }

    public void ShowBossPage()
    {
        currentPage?.SetActive(false);
        currentPage = bossPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(false);

        rightButton.SetActive(false);
    }

    public void ShowAchievementPage()
    {
        currentPage?.SetActive(false);
        currentPage = achievementPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowGameModeMenuPage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "BACK";

        rightButton.SetActive(false);
    }

    public void ShowQuestMenuPage()
    {
        currentPage?.SetActive(false);
        currentPage = questMenuPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowGameModeMenuPage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "BACK";

        rightButton.SetActive(false);
    }

    public void ShowTypingQuestPage()
    {
        currentPage?.SetActive(false);
        currentPage = typingQuestPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowQuestMenuPage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "BACK";

        rightButton.SetActive(false);
    }

    public void ShowTypingQuestResultPage()
    {
        currentPage?.SetActive(false);
        currentPage = typingQuestResultPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowQuestMenuPage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "BACK";

        rightButton.SetActive(false);
    }

    private void CountPlayTime()
    {

    }
}
