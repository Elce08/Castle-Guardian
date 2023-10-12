using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoad : MonoBehaviour
{
    static string sceneName;

    static Slider slider;
    static TextMeshProUGUI LoadingText;
    static Image randomCharacterImage;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// ¾À ·Îµå ÇÔ¼ö
    /// </summary>
    /// <param name="sceneName">¾À ÀÌ¸§</param>
    public static void OnSceneLoad(string SceneName)
    {
        sceneName = SceneName;
        SceneManager.LoadScene("LoadScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        float timer = 0.0f;
        slider = GetComponentInChildren<Slider>();
        slider.value = 0.0f;
        LoadingText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        randomCharacterImage = transform.GetChild(3).GetComponent<Image>();
        SetImage();
        StartCoroutine(LoadingTextProgress());

        while(!operation.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (operation.progress < 0.9f)
            {
                slider.value = Mathf.Lerp(slider.value, operation.progress, timer);
                if(slider.value > operation.progress)
                {
                    timer = 0.0f;
                }
            }
            else
            {
                slider.value = Mathf.Lerp(slider.value, 1.0f, timer);
                if(slider.value > 0.999)
                {
                    slider.value = 1.0f;
                    yield return new WaitForSeconds(0.1f);
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private IEnumerator LoadingTextProgress()
    {
        float waitTime = 0.2f;
        WaitForSeconds wait = new(waitTime);

        string[] texts =
        {
            "Loading",
            "Loading .",
            "Loading . .",
            "Loading . . .",
            "Loading . . . .",
            "Loading . . . . ."
        };

        int index = 0;

        while (true)
        {
            LoadingText.text = texts[index];
            index++;
            index %= texts.Length;

            yield return wait;
        }
    }

    private void SetImage()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        int random = UnityEngine.Random.Range(0, 6);
        randomCharacterImage.sprite = gameManager.playerImages[random];
    }
}
