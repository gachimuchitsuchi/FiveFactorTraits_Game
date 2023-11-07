using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMenuManager : MonoBehaviour
{
    public static LevelMenuManager instance
    {
        get;
        private set;
    }

    [field: SerializeField, RenameField("PlayerLevelText")]
    private TextMeshProUGUI playerLevelText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("PlayerExpText")]
    private TextMeshProUGUI playerExpText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("PlayerNameText")]
    private TextMeshProUGUI playerNameText
    {
        get;
        set;
    }

    private const int NUMBER_OF_LEVELS = 5;

    [field: SerializeField, RenameField("LevelButtons")]
    private List<GameObject> levelButtons
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
        //各ボタンにレベルごとのShowExaminationPageを追加
        for (int i = 0; i < levelButtons.Count; i++)
        {
            ExaminationManager.ExaminationLevel num = (ExaminationManager.ExaminationLevel)(i + 1);
            levelButtons[i].GetComponent<Button>().onClick.AddListener(() => GameManager.instance.ShowExaminationPage(num));
        }
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
        playerLevelText.text = "Lv" + PlayerDataManager.instance.playerData.level;
        playerNameText.text = PlayerDataManager.instance.playerData.playerName;
        playerExpText.text = "Exp" + PlayerDataManager.instance.playerData.exp;

        for(int i=0; i < levelButtons.Count; i++)
        {
            int buttonLevel = i + 1;
            if (PlayerDataManager.instance.playerData.level < buttonLevel)
            {
                levelButtons[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                if(PlayerDataManager.instance.playerData.isCorrectAllWordsPerLevel[i]) levelButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public static string StringizeIsCorrectAllWordsPerLevel(List<bool> list)
    {
        string listString = "";

        foreach(bool flg in list)
        {
            string correctly = flg.ToString();
            listString += correctly + ",";
        }

        return listString;
    }

    public static List<bool> DestringizeIsCorrectAllWordsPerLevel(string listString)
    {
        List<bool> list = new List<bool>();

        List<string> contents = new List<string>();
        contents.AddRange(listString.Split(","));

        foreach(string splitContent in contents)
        {
            if(splitContent != null && splitContent != "" && !splitContent.Contains("\0"))
            {
                list.Add(bool.Parse(splitContent));
            }
        }

        return list;
    }
}
