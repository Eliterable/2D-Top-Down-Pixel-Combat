using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowColorChanger : MonoBehaviour
{
    public float speed = 1.0f; // Prędkość zmiany koloru
    private SpriteRenderer spriteRenderer; // SpriteRenderer obiektu

    private float hue; // Wartość hue dla tęczy

    void Start()
    {
        // Pobierz SpriteRenderer obiektu
        spriteRenderer = GetComponent<SpriteRenderer>();
        hue = 0; // Początkowa wartość hue
    }

    void Update()
    {
        // Zwiększ wartość hue na podstawie upływu czasu i prędkości
        hue += Time.deltaTime * speed;

        // Upewnij się, że hue nie przekracza 1 (co odpowiada 360 stopniom w kołowej przestrzeni kolorów)
        if (hue > 1) hue -= 1;

        // Przekształć hue na kolor RGB i ustaw go na kolor materiału
        spriteRenderer.color = Color.HSVToRGB(hue, 1, 1);
    }
}