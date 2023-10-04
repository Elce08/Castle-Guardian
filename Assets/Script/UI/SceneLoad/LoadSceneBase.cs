using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneBase : MonoBehaviour
{
    public string sceneName;

    Button LoadSceneButton;

    protected virtual void Awake()
    {
        LoadSceneButton = GetComponent<Button>();
    }

    private void Start()
    {
        LoadSceneButton.onClick.AddListener(SceneLoad);
    }

    private void SceneLoad()
    {
        AsyncLoad.OnSceneLoad(sceneName);
    }
}
