using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeMenuManager : MonoBehaviour
{
    public static GameModeMenuManager instance
    {
        get;
        private set;
    }

    [field: SerializeField, RenameField("LevelButton")]
    private GameObject levelButton
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
        levelButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowLevelMenuPage);
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
