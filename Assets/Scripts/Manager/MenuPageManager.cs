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
                descriptionText.text = "英単語リストを閲覧します";
                break;
            case CursorTarget.PlayButton:
                descriptionText.text = "英単語の学習を始めます";
                break;
            case CursorTarget.ExaminationButton:
                descriptionText.text = "英単語のテストを受験します";
                break;
        }
    }
}
