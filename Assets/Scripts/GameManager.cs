using System.Collections;
using System.Collections.Generic;
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
        SetAllPagesActive(true);
    }

    private void Start()
    {
        SetAllPagesActive(false);
        ShowTitlePage();
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void SetAllPagesActive(bool active)
    {
        titlePage.SetActive(active);
        playerDataPage.SetActive(active);
        startPage.SetActive(active);
        ffqPage.SetActive(active);
        ffqResultPage.SetActive(active);
        menuPage.SetActive(active);
        examinationPage.SetActive(active);
        examinationResultPage.SetActive(active);
        wordListPage.SetActive(active);
        gameModeMenuPage.SetActive(active);
        examinationMenuPage.SetActive(active);
    }

    public void Play()
    {
        ShowStartPage();
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
        rightButton.GetComponent<Button>().onClick.AddListener(ShowExaminationPage);
        rightButton.GetComponentInChildren<TextMeshProUGUI>().text = "NEXT";
    }

    public void ShowExaminationPage()
    {
        currentPage?.SetActive(false);
        currentPage = examinationPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(false);

        rightButton.SetActive(true);
        rightButton.GetComponent<Button>().onClick.RemoveAllListeners();
        rightButton.GetComponent<Button>().onClick.AddListener(ShowExaminationResultPage);
        rightButton.GetComponentInChildren<TextMeshProUGUI>().text = "NEXT";
    }

    public void ShowExaminationResultPage()
    {
        currentPage?.SetActive(false);
        currentPage = examinationResultPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(false);

        rightButton.SetActive(true);
        rightButton.GetComponent<Button>().onClick.RemoveAllListeners();
        rightButton.GetComponent<Button>().onClick.AddListener(ShowMenuPage);
        rightButton.GetComponentInChildren<TextMeshProUGUI>().text = "NEXT";
    }

    public void ShowMenuPage()
    {
        currentPage?.SetActive(false);
        currentPage = menuPage;
        currentPage?.SetActive(true);

        leftButton.SetActive(true);
        leftButton.GetComponent<Button>().onClick.RemoveAllListeners();
        leftButton.GetComponent<Button>().onClick.AddListener(ShowTitlePage);
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "Title";

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
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";

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
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";

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
        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";

        rightButton.SetActive(false);
    }
}
