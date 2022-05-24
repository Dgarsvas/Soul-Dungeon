using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField]
    private LevelManagerScriptableObject levels;

    private const float FADE_TIME = 0.8f;
   
    public delegate void FadeInFinishedEvent();
    public static event FadeInFinishedEvent OnFadeInFinished;

    public static SceneLoadManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return FadeOut();
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        yield return op;
        yield return FadeIn();
        OnFadeInFinished?.Invoke();
    }

    private IEnumerator FadeOut()
    {
        float timer = FADE_TIME;
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
        }
    }

    private IEnumerator FadeIn()
    {
        float timer = FADE_TIME;
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
        }
    }
}