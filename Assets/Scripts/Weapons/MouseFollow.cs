using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    SpriteRenderer sr;
    GameObject sword;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        FaceMouse();
    }


    private void FaceMouse()
    {

        if (!GetComponentInChildren<Sword>())
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 direction = transform.position - mousePosition;
            transform.right = -direction;
        }
    }

}
