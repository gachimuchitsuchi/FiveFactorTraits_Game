using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenuManager : MonoBehaviour
{
    public static QuestMenuManager instance
    {
        get;
        private set;
    }

    [field: SerializeField, RenameField("TypingQuestButton")]
    private GameObject typingQuestButton
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
        Initialize();
    }

    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Initialize()
    {
        typingQuestButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowTypingQuestPage);
    }
}
