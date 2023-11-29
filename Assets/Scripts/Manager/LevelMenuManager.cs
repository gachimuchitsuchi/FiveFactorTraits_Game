using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMenuManager : MonoBehaviour
{
    private enum CursorTarget
    {
        Nothing,
        Level1Button,
        Level2Button,
        Level3Button,
        Level4Button,
        Level5Button
    }

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

    [field: SerializeField, RenameField("DescriptionText")]
    private TextMeshProUGUI descriptionText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("DescriptionButton")]
    private GameObject descriptionButton
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

    private void Awake()
    {
        CreateInstance();
    }

    private void Start()
    {
        descriptionButton.GetComponent<Button>().onClick.AddListener(OpenDescriptionPanel);

        GameObject backButton = descriptionPanel.transform.Find("BackButton").gameObject;
        backButton.GetComponent<Button>().onClick.AddListener(CloseDescriptionPanel);

        //�e�{�^���Ƀ��x�����Ƃ�ShowExaminationPage��ǉ�
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

        descriptionText.text = "";

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

    private void OpenDescriptionPanel()
    {
        descriptionPanel.SetActive(true);
        descriptionPanel.GetComponent<AnimatedDialog>().Open();
    }

    private void CloseDescriptionPanel()
    {
        descriptionPanel.SetActive(false);
        descriptionPanel.GetComponent<AnimatedDialog>().Close();
    }

    public void ShowDescription(int cursorTarget)
    {
        switch ((CursorTarget)cursorTarget)
        {
            case CursorTarget.Nothing:
                descriptionText.text = "";
                break;
            case CursorTarget.Level1Button:
                descriptionText.text = "���x��1�̉p�P����w�K���܂�";
                break;
            case CursorTarget.Level2Button:
                if (levelButtons[1].GetComponent<Button>().interactable) descriptionText.text = "���x��2�̉p�P����w�K���܂�";
                else descriptionText.text = "�v���C���[���x����2�ȏ�&���x��1�̃e�X�g�𖞓_�ŉ��";
                break;
            case CursorTarget.Level3Button:
                if (levelButtons[2].GetComponent<Button>().interactable) descriptionText.text = "���x��3�̉p�P����w�K���܂�";
                else descriptionText.text = "�v���C���[���x����3�ȏ�&���x��2�̃e�X�g�𖞓_�ŉ��";
                break;
            case CursorTarget.Level4Button:
                if (levelButtons[3].GetComponent<Button>().interactable) descriptionText.text = "���x��4�̉p�P����w�K���܂�";
                else descriptionText.text = "�v���C���[���x����4�ȏ�&���x��3�̃e�X�g�𖞓_�ŉ��";
                break;
            case CursorTarget.Level5Button:
                if (levelButtons[4].GetComponent<Button>().interactable) descriptionText.text = "���x��5�̉p�P����w�K���܂�";
                else descriptionText.text = "�v���C���[���x����5�ȏ�&���x��4�̃e�X�g�𖞓_�ŊJ��";
                break;
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
