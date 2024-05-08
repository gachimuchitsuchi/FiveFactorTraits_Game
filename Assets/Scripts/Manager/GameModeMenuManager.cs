using System;
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
        QuestButton,
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

    [field: SerializeField, RenameField("QuestButton")]
    private GameObject questButton
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

    private void OnEnable()
    {
        ShowButton();
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
        questButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowQuestMenuPage);
        levelButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowLevelMenuPage);
        bossButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowBossPage);
        achievementButton.GetComponent<Button>().onClick.AddListener(GameManager.instance.ShowAchievementPage);

        descriptionText.text = "";
    }

    private void ShowButton()
    {
        questButton.SetActive(false);
        achievementButton.SetActive(false);
        levelButton.SetActive(false);
        bossButton.SetActive(false);


        foreach (FiveFactorQuestionManager.PlayerType playerType in Enum.GetValues(typeof(FiveFactorQuestionManager.PlayerType)))
        {
            if (PlayerDataManager.instance.playerData.isActiveGameElements[playerType])
            {
                switch (playerType)
                {
                    case FiveFactorQuestionManager.PlayerType.Aesthietic:
                        questButton.SetActive(true);
                        break;
                    case FiveFactorQuestionManager.PlayerType.Narrative:
                        achievementButton.SetActive(true);
                        break;
                    case FiveFactorQuestionManager.PlayerType.Goal:
                        levelButton.SetActive(true);
                        break;
                    case FiveFactorQuestionManager.PlayerType.Challenge:
                        //bossButton.SetActive(true);
                        break;
                    case FiveFactorQuestionManager.PlayerType.Social:
                        break;
                }
            }
        }
    }

    public void ShowDescription(int cursorTarget)
    {
        switch ((CursorTarget)cursorTarget)
        {
            case CursorTarget.Nothing:
                descriptionText.text = "";
                break;
            case CursorTarget.QuestButton:
                descriptionText.text = "クエストに挑戦します";
                break;
            case CursorTarget.AchievementButton:
                descriptionText.text = "実績を閲覧します";
                break;
            case CursorTarget.LevelButton:
                descriptionText.text = "レベル上げを行いながら英単語を学習します";
                break;
            case CursorTarget.BossButton:
                descriptionText.text = "ボスを倒しながら英単語を学習します";
                break;
        }
    }
}
