using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager Instance
    {
        get;
        private set;
    }

    [SerializeField]
    private LevelManagerScriptableObject levels;
    [SerializeField]
    private Image fade;
    [SerializeField]
    private AnimationCurve curve;

    private const float FADE_TIME = 0.8f;
   
    public delegate void FadeInFinishedEvent();
    public static event FadeInFinishedEvent OnFadeInFinished;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
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
        float timer = 0;
        while (timer < FADE_TIME)
        {
            yield return null;
            timer += Time.deltaTime;
            fade.color = Color.Lerp(Color.clear, Color.black, curve.Evaluate(timer / FADE_TIME));
        }

        fade.color = Color.black;
    }

    private IEnumerator FadeIn()
    {
        float timer = 0;
        while (timer < FADE_TIME)
        {
            yield return null;
            timer += Time.deltaTime;
            fade.color = Color.Lerp(Color.black, Color.clear, curve.Evaluate(timer / FADE_TIME));
        }

        fade.color = Color.clear;
    }
}