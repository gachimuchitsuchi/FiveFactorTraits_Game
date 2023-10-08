using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordListManager : MonoBehaviour
{
    public static WordListManager instance
    {
        get;
        private set;
    }

    [field: SerializeField, RenameField("WordListContent")]
    private GameObject wordListContent
    {
        get;
        set;
    }

    [field: SerializeField, RenameField("Word Prefab")]
    private GameObject wordPrefab
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
        if(instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        //wordListContent��Transform�����q���S�ĂɃA�N�Z�X���A�폜
        foreach(Transform element in wordListContent.transform)
        {
            Destroy(element.gameObject);
        }

        int count = 0;
        foreach(Word word in GameManager.instance.words)
        {
            //�X�N���[���r���[�ɕK�v�ȃ��[�h�̃{�[�h��wrodListContent�̎q�K�w�ɍ쐬
            GameObject wordElement = Instantiate(wordPrefab, wordListContent.transform);

            wordElement.GetComponent<WordBehaviour>().word = word;
            wordElement.GetComponent<WordBehaviour>().UpdateWord();

            if(count % 2 == 0)
            {
                wordElement.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
            }
            else
            {
                wordElement.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f);
            }
            count++;
        }
    }
}
