using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementBehaviour : MonoBehaviour
{
    public Achievement achievement
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("MedalImage")]
    private Image medalImage
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("NameText")]
    private TextMeshProUGUI nameText
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("DescriptionText")]
    private TextMeshProUGUI descriptionText
    {
        get;
        set;
    }

    public void UpdateAchievement()
    {
        Color color;
        if (achievement.unlocked)
        {
            ColorUtility.TryParseHtmlString("#FFD600", out color);
            medalImage.color = color;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#7D7D7D", out color);
            medalImage.color = color;
        }

        nameText.text = achievement.name;
        descriptionText.text = achievement.description;
    }
}
