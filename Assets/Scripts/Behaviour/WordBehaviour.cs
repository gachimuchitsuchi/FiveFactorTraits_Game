using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordBehaviour : MonoBehaviour
{
    public Word word
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("EnglishText")]
    private TextMeshProUGUI englishText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("JapaneseText")]
    private TextMeshProUGUI japaneseText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("LevelText")]
    private TextMeshProUGUI levelText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Checkmark")]
    private GameObject checkmark
    {
        get;
        set;
    }

    public void UpdateWord()
    {
        englishText.text = word.english;
        japaneseText.text = word.japanese;
        levelText.text = "Lv" + word.level;

        if (!PlayerDataManager.instance.playerData.isActiveGameElements[FiveFactorQuestionManager.PlayerType.Goal])
        {
            levelText.text = "";
        }

        if(word.answeredCorrectly && PlayerDataManager.instance.playerData.isActiveGameElements[FiveFactorQuestionManager.PlayerType.Narrative])
        {
            checkmark.SetActive(true);
        }
        else
        {
            checkmark.SetActive(false);
        }
    }
}
