using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoad : MonoBehaviour
{
    public int nextSceneNumber = 0;

    float loadRatio;

    public float loadingBarSpeed = 1.0f;

    bool loadingDone = false;

    Slider slider;
    Button button;
    AsyncOperation async;

    private void Start()
    {
        async = SceneManager.LoadSceneAsync(nextSceneNumber);
        button = FindObjectOfType<Button>();
        slider = FindObjectOfType<Slider>();
        // 캐릭터 달리는 애니메이션 추가
        button.gameObject.SetActive(false);
        StartCoroutine(LoadScene());
    }

    private void Update()
    {
        if (slider.value < loadRatio)
        {
            slider.value += (Time.deltaTime * loadingBarSpeed);
        }
    }

    void SceneLoad()
    {
        if (loadingDone)
        {
            async.allowSceneActivation = true;
            SceneManager.LoadScene(nextSceneNumber);
            Debug.Log("Loaded");
        }
    }

    public void SetSceneNumber(int sceneNumber)
    {
        nextSceneNumber = sceneNumber;
    }

    IEnumerator LoadScene()
    {
        slider.value = 0.0f;
        loadRatio = 0.0f;

        async.allowSceneActivation = false;
        while (loadRatio < 1.0f)
        {
            loadRatio = async.progress + 0.1f;
            yield return null;
        }

        yield return new WaitForSeconds((loadRatio - slider.value) / loadingBarSpeed);

        button.gameObject.SetActive(true);
        button.onClick.AddListener(SceneLoad);
        loadingDone = true;
    }
}
