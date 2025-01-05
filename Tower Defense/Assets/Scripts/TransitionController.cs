using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    private float _fadeDuration = 2f;
    private Coroutine _fadeCoroutine;
    private void Start()
    {
        StartCoroutineFadeIn();
    }

    public void StartTransition()
    {
        StartCoroutineFadeOut();
    }

    private IEnumerator FadeOut()
    {
        float t = 0f;
        Color color = fadeImage.color;
        color.a = 0f;
        
        while (t < _fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / _fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
        _fadeCoroutine = null;
    }
    
    private IEnumerator FadeIn()
    {
        float t = 0f;
        Color color = fadeImage.color;
        color.a = 1f;
        
        while (t < _fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / _fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        
        color.a = 0f;
        fadeImage.color = color;
        _fadeCoroutine = null;
    }

    public void StartCoroutineFadeIn()
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
        CheckTimeScale();
        _fadeCoroutine = StartCoroutine(FadeIn());
    }
    private void StartCoroutineFadeOut()
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
        CheckTimeScale();
        _fadeCoroutine = StartCoroutine(FadeOut());
    }

    private void CheckTimeScale()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
        }
    }
}