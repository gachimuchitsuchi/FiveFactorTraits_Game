using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExaminationMenuManager : MonoBehaviour
{
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

    private void Awake()
    {
        CreateInstance();
    }

    private void Start()
    {
        practiceExaminationButton.GetComponent<Button>().onClick.AddListener(StartPracticeExamination);
        finalExaminationButton.GetComponent<Button>().onClick.AddListener(StartFinalExamination);
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void StartPracticeExamination()
    {
        GameManager.instance.ShowExaminationPage();
    }

    private void StartFinalExamination()
    {
        GameManager.instance.ShowExaminationPage();
    }
}
