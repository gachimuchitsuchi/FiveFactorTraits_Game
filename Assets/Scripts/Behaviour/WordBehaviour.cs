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

    public void UpdateWord()
    {
        englishText.text = word.english;
        japaneseText.text = word.japanese;
    }
}
