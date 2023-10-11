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

    [field: SerializeField, RenameField("LevelButtons")]
    private List<GameObject> levelButtons
    {
        get;
        set;
    }

    public List<bool> isCorrectLevelsAllWords
    {
        get;
        set;
    }

    private void Awake()
    {
        CreateInstance();
        isCorrectLevelsAllWords = new List<bool>(5);
        for(int i=0; i<5; i++)
        {
            if (i == 0) isCorrectLevelsAllWords.Add(true);
            else isCorrectLevelsAllWords.Add(false);
        }
    }

    private void Start()
    {
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
                if(isCorrectLevelsAllWords[i]) levelButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }
}
