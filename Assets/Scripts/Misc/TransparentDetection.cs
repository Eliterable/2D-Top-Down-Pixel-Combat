using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float transparencyAmount = 0.8f;
    [SerializeField] float fadeTime = 0.4f;
    private float startTransparencyAmount = 1f;
    SpriteRenderer sr;
    Tilemap tilemap;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (sr)
            {
                StartCoroutine(Fadeing(sr, fadeTime, sr.color.a, transparencyAmount));
            }
            else if (tilemap)
            {
                StartCoroutine(Fadeing(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
            }
        }
    }

    private IEnumerator Fadeing(SpriteRenderer sr, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
            yield return null;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
            if (sr)
            {
                StartCoroutine(Fadeing(sr, fadeTime, sr.color.a, startTransparencyAmount));
            }
            else if (tilemap)
            {
                StartCoroutine(Fadeing(tilemap, fadeTime, tilemap.color.a, startTransparencyAmount));
            }
    }
    private IEnumerator Fadeing(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }




}

