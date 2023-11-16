using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExaminationMenuManager : MonoBehaviour
{
    private enum CursorTarget
    {
        Nothing,
        PracticeButton,
        FinalButton
    }
    public static ExaminationMenuManager instance
    {
        get;
        private set;
    }

    [field: SerializeField, RenameField("PracticeExaminationButton")]
    private GameObject practiceExaminationButton
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("FinalExaminationButton")]
    private GameObject finalExaminationButton
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

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void InitializeUI()
    {
        practiceExaminationButton.GetComponent<Button>().onClick.AddListener(StartPracticeExamination);
        finalExaminationButton.GetComponent<Button>().onClick.AddListener(StartFinalExamination);

        descriptionText.text = "";
    }

    private void StartPracticeExamination()
    {
        GameManager.instance.ShowExaminationPage(ExaminationManager.ExaminationLevel.All);
    }

    private void StartFinalExamination()
    {
        PlayerDataManager.instance.playerData.gamePhase = PlayerData.GamePhase.FinalExamination;
        GameManager.instance.ShowExaminationPage(ExaminationManager.ExaminationLevel.All);
    }

    public void ShowDescription(int cursorTarget)
    {
        switch ((CursorTarget)cursorTarget)
        {
            case CursorTarget.Nothing:
                descriptionText.text = "";
                break;
            case CursorTarget.PracticeButton:
                descriptionText.text = "模擬試験を始めます";
                break;
            case CursorTarget.FinalButton:
                descriptionText.text = "最終試験を始めます\n始めると戻ることができません";
                break;
        }
    }
}
