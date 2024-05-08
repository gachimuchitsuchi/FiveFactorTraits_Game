using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMenuManager : MonoBehaviour
{
    public static BossMenuManager instance
    {
        get;
        private set;
    }

    [field: SerializeField, RenameField("PlayButton")]
    private Button playButton
    {
        get;
        set;
    }

    private void Awake()
    {
        CreateInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(GameManager.instance.ShowBossPage);
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
