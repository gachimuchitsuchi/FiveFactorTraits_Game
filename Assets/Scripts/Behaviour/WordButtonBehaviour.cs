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

    public bool isAnswer
    {
        get;
        set;
    }

    private void Awake()
    {
        UpdateWord();
        Initialize();
    }

    private void Initialize()
    {
        isAnswer = false;
    }

    public void UpdateWord()
    {
        if (word != null)
        {
            wordText.text = word.english;
        }
    }
}
