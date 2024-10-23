using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelectionManager : MonoBehaviour
{
    public static ModeSelectionManager instance
    {
        get;
        private set;
    }

    [field: SerializeField, RenameField("AButton")]
    private GameObject aButton
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("BButton")]
    private GameObject bButton
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("CButton")]
    private GameObject cButton
    {
        get;
        set;
    }

    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        aButton.GetComponent<Button>().onClick.AddListener(() => SetGroupA());
        bButton.GetComponent<Button>().onClick.AddListener(() => SetGroupB());
        cButton.GetComponent<Button>().onClick.AddListener(() => SetGroupC());
    }

    /*
    private void SetGameMode(bool isUsing)
    {
        PlayerDataManager.instance.playerData.isUsingGameElements = isUsing;

        GameManager.instance.ShowMenuPage();
    }
    */

    private void SetGroupA()
    {
        PlayerDataManager.instance.playerData.group = PlayerData.Group.A;
        GameManager.instance.ShowMenuPage();
    }

    private void SetGroupB()
    {
        PlayerDataManager.instance.playerData.group = PlayerData.Group.B;
        GameManager.instance.ShowMenuPage();
    }

    private void SetGroupC()
    {
        PlayerDataManager.instance.playerData.group = PlayerData.Group.C;
        GameManager.instance.ShowMenuPage();
    }
}
