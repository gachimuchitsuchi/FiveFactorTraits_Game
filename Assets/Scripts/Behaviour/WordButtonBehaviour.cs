using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordButtonBehaviour : MonoBehaviour
{
    public Word word
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Word Text")]
    private TextMeshProUGUI wordText
    {
        get;
        set;
    }

    public char spell
    {
        get;
        set;
    }

    public bool isAnswer
    {
        get;
        set;
    }

    private void Awake()
    {
        UpdateEnglishWord();
        Initialize();
    }

    private void Initialize()
    {
        isAnswer = false;
    }

    public void UpdateEnglishWord()
    {
        if (word != null)
        {
            wordText.text = word.english;
        }
    }

    public void UpdateJapaneseWord()
    {
        if(word != null)
        {
            wordText.text = word.japanese;
        }
    }

    public void UpdateSpell()
    {
        if(spell != ' ')
        {
            wordText.text = spell.ToString();
        }
    }

}
