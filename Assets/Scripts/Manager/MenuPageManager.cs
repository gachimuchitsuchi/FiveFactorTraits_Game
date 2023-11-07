using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuPageManager : MonoBehaviour
{
    public enum CursorTarget
    {
        Nothing,
        WordbookButton,
        PlayButton,
        ExaminationButton
    }

    public static MenuPageManager instance
    {
        get;
        private set;
    }

    [field: SerializeField, RenameField("WordListButton")]
    private GameObject wordListButton
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("PlayButton")]
    private GameObject playButton
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("ExaminationMenuButton")]
    private GameObject examinationMenuButton
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Description Text")]
    private TextMeshProUGUI descriptionText
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
        InitializeUI();
    }

    private void InitializeUI()
    {
        wordListButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowWordListPage);
        playButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowGameModeMenuPage);
        examinationMenuButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowExaminationMenuPage);

        descriptionText.text = "";
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ShowDescription(int cursorTarget)
    {
        switch ((CursorTarget)cursorTarget)
        {
            case CursorTarget.Nothing:
                descriptionText.text = "";
                break;
            case CursorTarget.WordbookButton:
                descriptionText.text = "�p�P�ꃊ�X�g���{�����܂�";
                break;
            case CursorTarget.PlayButton:
                descriptionText.text = "�p�P��̊w�K���n�߂܂�";
                break;
            case CursorTarget.ExaminationButton:
                descriptionText.text = "�p�P��̃e�X�g���󌱂��܂�";
                break;
        }
    }
}
