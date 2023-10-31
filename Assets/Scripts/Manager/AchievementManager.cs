using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance
    {
        get;
        private set;
    }

    public List<Achievement> achievements
    {
        get;
        private set;
    }

    private const string achievementsFilePath = "Data/achievements";
    private const string achievementResultFileName = "achievement_result.csv";

    [field: SerializeField, RenameField("Achievement List Content")]
    private GameObject achievementListContent
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Achievement Prefab")]
    private GameObject achievementPrefab
    {
        get;
        set;
    }

    private void Awake()
    {
        CreateInstance();
        Initialize();
    }

    private void OnEnable()
    {
        ShowAchivements();
        UnlockAchievement(1);
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Initialize()
    {
        ReadCsvAchievements(achievementsFilePath);
    }

    private void ReadCsvAchievements(string filePath)
    {
        achievements = new List<Achievement>();
        StringReader stringReader = new StringReader(((TextAsset)Resources.Load(filePath)).text);

        while(stringReader.Peek() != -1)
        {
            string[] values = stringReader.ReadLine().Split(',');
            Debug.Log(values[0]);
            achievements.Add(new Achievement(int.Parse(values[0]), values[1], values[2]));
        }
    }

    private void ShowAchivements()
    {
        foreach (Transform element in achievementListContent.transform)
        {
            Destroy(element.gameObject);
        }

        int count = 0;
        foreach (Achievement achievement in achievements)
        {
            GameObject achievementElement = Instantiate(achievementPrefab, achievementListContent.transform);

            achievementElement.GetComponent<AchievementBehaviour>().achievement = achievement;
            achievementElement.GetComponent<AchievementBehaviour>().UpdateAchievement();

            if (count % 2 == 0)
            {
                achievementElement.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
            }
            else
            {
                achievementElement.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f);
            }

            count++;
        }
    }

    public void UnlockAchievement(int id)
    {
        achievements.FirstOrDefault(n => n.id == id).Unlock();
    }
}
