using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingQuestResultManager : MonoBehaviour
{
    public static TypingQuestResultManager instance
    {
        get;
        private set;
    }

    [field: SerializeField, RenameField("QuestTitle")]
    private TextMeshProUGUI questTitle
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("TimeScoreText")]
    private TextMeshProUGUI timeScoreText
    {
        get;
        set;
    }

    private void Awake()
    {
        CreateInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ShowResult(float timeScore, int examinationWordsCount)
    {
        timeScoreText.text = timeScore.ToString("f1") + "秒";
        questTitle.text = examinationWordsCount + "問" + " " + "タイピング";
    }
}
