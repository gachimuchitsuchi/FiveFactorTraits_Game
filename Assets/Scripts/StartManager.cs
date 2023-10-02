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
            ShowWarningText("�����O�����͂���Ă��܂���");
            return;
        }
        if (!CheckInvalidCharacters(playerNameInputField.text))
        {
            ShowWarningText("���g�p�ł��Ȃ��������܂܂�Ă��܂�");
            return;
        }
        if(!CheckInvalidStringSize(playerNameInputField.text, maximumPlayerNameSize))
        {
            ShowWarningText("�����͉\�ȕ������𒴂��Ă��܂�");
            return;
        }

        warningText.gameObject.SetActive(false);

        PlayerDataManager.instance.playerData.playerName = playerNameInputField.text;

        GameManager.instance.ShowFiveFactorQuestionPage();
    }

    //inputString���ɖ����ȕ������܂܂�Ă��邩�`�F�b�N����
    private bool CheckInvalidCharacters(string inputString)
    {
        List<char> invalidCharacters = new List<char>();
        //System.IO.Path.GetInvalidFileNameChars() ���\�b�h���g�p���A
        //�t�@�C�����Ƃ��Ďg�p�ł��Ȃ������̔z����擾���A������ invalidCharacters ���X�g�ɒǉ�
        //�t�@�C���V�X�e���ŋ�����Ă��Ȃ����� ��F"/", "", ":", "*" �Ȃ�
        invalidCharacters.AddRange(System.IO.Path.GetInvalidFileNameChars());
        //#���ǉ�
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
