using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class FadeingScene : Singleton<FadeingScene>
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed = 1f;

    public void FadetoBlack()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        fadeRoutine = FadeRoutine(1);
        StartCoroutine(fadeRoutine);
    }

    public void FadetoClear()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        fadeRoutine = FadeRoutine(0);
        StartCoroutine(fadeRoutine);
    }


    IEnumerator fadeRoutine;



    IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }
    }



}
