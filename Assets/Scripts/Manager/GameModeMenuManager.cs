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

    [field: SerializeField, RenameField("BossButton")]
    private GameObject bossButton
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("AchievementButton")]
    private GameObject achievementButton
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
        bossButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowBossPage);
        achievementButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowAchievementPage);
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
