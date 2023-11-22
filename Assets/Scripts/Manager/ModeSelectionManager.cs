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

    [field: SerializeField, RenameField("YesButton")]
    private GameObject yesButton
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("NoButton")]
    private GameObject noButton
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
        yesButton.GetComponent<Button>().onClick.AddListener(() => SetGameMode(true));
        noButton.GetComponent<Button>().onClick.AddListener(() => SetGameMode(false));
    }

    private void SetGameMode(bool isUsing)
    {
        PlayerDataManager.instance.playerData.isUsingGameElements = isUsing;

        GameManager.instance.ShowMenuPage();
    }
}
