using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeComponent : MonoBehaviour
{
    public bool FadeOnStart = false;
    public Image targetGraphics;
    public float FadeDuration = 2f;

    public UnityEvent afterFade;

    public void Start()
    {
        if(FadeOnStart)
            StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float time = 0;
        while (time < FadeDuration)
        {
            time += Time.deltaTime;
            targetGraphics.CrossFadeAlpha(1-time/FadeDuration,0,true);
            yield return null;
        }
        targetGraphics.CrossFadeAlpha(0,0,true);
    }

    IEnumerator FadeOut()
    {
        float time = 0;
        while (time < FadeDuration)
        {
            time += Time.deltaTime;
            targetGraphics.CrossFadeAlpha(time/FadeDuration,0,true);
            yield return null;
        }
        targetGraphics.CrossFadeAlpha(1,0,true);
        
        afterFade?.Invoke();
    }
}
