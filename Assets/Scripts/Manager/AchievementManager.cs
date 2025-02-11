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

        foreach (Achievement achievement in achievements)
        {
            if (achievement.id != 12 && !achievement.unlocked)
            {
                return;
            }
        }

        UnlockAchievement(12);
    }

    public void CntUnlockAchivement()
    {
        int cnt = 0;
        foreach(Achievement achievement in achievements)
        {
            if (achievement.unlocked)
            {
                cnt++;
            }
        }
        PlayerDataManager.instance.playerData.unlockAchiveCnt = cnt;
    }

    //save
    public static string StringizeCorrectlyAnsweredWords(Dictionary<Word, bool> dictionary)
    {
        string dictionaryString = "";

        foreach (KeyValuePair<Word, bool> pair in dictionary)
        {
            string word = pair.Key.english;
            string answeredCorrectly = pair.Value.ToString();

            dictionaryString += word + "," + answeredCorrectly + "|";
        }

        return dictionaryString;
    }

    //load
    public static Dictionary<Word, bool> DestringizeCorrectlyAnsweredWords(string dictionaryString)
    {
        Dictionary<Word, bool> dictionary = new Dictionary<Word, bool>();

        List<string> contents = new List<string>();
        contents.AddRange(dictionaryString.Split('|'));

        foreach (string splitContent in contents)
        {
            if (splitContent != null && splitContent != "" && !splitContent.Contains("\0"))
            {
                Word word = GameManager.instance.words.First(item => item.english == splitContent.Split(',')[0]);
                bool answeredCorrectly = bool.Parse(splitContent.Split(',')[1]);

                if (word != null)
                {
                    dictionary.Add(word, answeredCorrectly);
                }
            }
        }

        return dictionary;
    }

    //save åãçá
    public static string StringizeUnlockedAchievements(Dictionary<Achievement, bool> dictionary)
    {
        string dictionaryString = "";

        foreach (KeyValuePair<Achievement, bool> pair in dictionary)
        {
            string word = pair.Key.name;
            string unlocked = pair.Value.ToString();

            dictionaryString += word + "," + unlocked + "|";
        }

        return dictionaryString;
    }

    //load ï™â
    public static Dictionary<Achievement, bool> DestringizeUnlockedAchievements(string dictionaryString)
    {
        Dictionary<Achievement, bool> dictionary = new Dictionary<Achievement, bool>();

        List<string> contents = new List<string>();
        contents.AddRange(dictionaryString.Split('|'));

        foreach (string splitContent in contents)
        {
            if (splitContent != null && splitContent != "" && !splitContent.Contains("\0"))
            {
                Achievement achievement = AchievementManager.instance.achievements.First(item => item.name == splitContent.Split(',')[0]);
                bool unlocked = bool.Parse(splitContent.Split(',')[1]);

                if (achievement != null)
                {
                    dictionary.Add(achievement, unlocked);
                }
            }
        }

        return dictionary;
    }
}
