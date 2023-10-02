using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuPageManager : MonoBehaviour
{
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

    private void Awake()
    {
        CreateInstance();
    }

    private void Start()
    {
        wordListButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowWordListPage);
        playButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowGameModeMenuPage);
        examinationMenuButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowExaminationMenuPage);
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
