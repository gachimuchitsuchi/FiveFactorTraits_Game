using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameModeMenuManager : MonoBehaviour
{
    private enum CursorTarget
    {
        Nothing,
        AchievementButton,
        LevelButton,
        BossButton,
    }

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

    [field: SerializeField, RenameField("Description Text")]
    private TextMeshProUGUI descriptionText
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
        InitializeUI();
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void InitializeUI()
    {
        levelButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowLevelMenuPage);
        bossButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowBossPage);
        achievementButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowAchievementPage);

        descriptionText.text = "";
    }

    public void ShowDescription(int cursorTarget)
    {
        switch ((CursorTarget)cursorTarget)
        {
            case CursorTarget.Nothing:
                descriptionText.text = "";
                break;
            case CursorTarget.AchievementButton:
                descriptionText.text = "ÀÑ‚ğ‰{——‚µ‚Ü‚·";
                break;
            case CursorTarget.LevelButton:
                descriptionText.text = "ƒŒƒxƒ‹ã‚°‚ğs‚¢‚È‚ª‚ç‰p’PŒê‚ğŠwK‚µ‚Ü‚·";
                break;
            case CursorTarget.BossButton:
                descriptionText.text = "ƒ{ƒX‚ğ“|‚µ‚È‚ª‚ç‰p’PŒê‚ğŠwK‚µ‚Ü‚·";
                break;
        }
    }
}
