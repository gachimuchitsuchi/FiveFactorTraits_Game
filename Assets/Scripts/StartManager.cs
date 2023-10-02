using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    public static StartManager instance
    {
        get;
        private set;
    }

    private const int maximumPlayerNameSize = 10;

    [field: SerializeField, RenameField("Player Name Input Field")]
    private TMP_InputField playerNameInputField
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Warning Text")]
    private TextMeshProUGUI warningText
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
        Initialize();  
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            SavePlayerName();
        }
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
        warningText.gameObject.SetActive(false);
    }

    public void SavePlayerName()
    {
        if(playerNameInputField.text.Length == 0)
        {
            ShowWarningText("※名前が入力されていません");
            return;
        }
        if (!CheckInvalidCharacters(playerNameInputField.text))
        {
            ShowWarningText("※使用できない文字が含まれています");
            return;
        }
        if(!CheckInvalidStringSize(playerNameInputField.text, maximumPlayerNameSize))
        {
            ShowWarningText("※入力可能な文字数を超えています");
            return;
        }

        warningText.gameObject.SetActive(false);

        PlayerDataManager.instance.playerData.playerName = playerNameInputField.text;

        GameManager.instance.ShowFiveFactorQuestionPage();
    }

    //inputString内に無効な文字が含まれているかチェックする
    private bool CheckInvalidCharacters(string inputString)
    {
        List<char> invalidCharacters = new List<char>();
        //System.IO.Path.GetInvalidFileNameChars() メソッドを使用し、
        //ファイル名として使用できない文字の配列を取得し、それらを invalidCharacters リストに追加
        //ファイルシステムで許可されていない文字 例："/", "", ":", "*" など
        invalidCharacters.AddRange(System.IO.Path.GetInvalidFileNameChars());
        //#も追加
        invalidCharacters.Add('#');

        if (inputString.IndexOfAny(invalidCharacters.ToArray()) < 0)
        {
            return true;
        }

        return false;
    }

    private bool CheckInvalidStringSize(string inputString, int maximumSize)
    {
        if (inputString.Length <= maximumSize)
        {
            return true;
        }

        return false;
    }

    private void ShowWarningText(string text)
    {
        warningText.text = text;
        warningText.gameObject.SetActive(true);
    }
}
