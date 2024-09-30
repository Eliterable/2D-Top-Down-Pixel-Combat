using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = 2f;
    CapsuleCollider2D cc;
    private float laserRange;
    SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CapsuleCollider2D>();

    }
    private void Start()
    {
        LaserFaceMouse();

    }
    public void UpdateLaserRange(float laserRange)
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLenghtRoutin());
    }
    private void LaserFaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
    }
    private IEnumerator IncreaseLaserLenghtRoutin()
    {
        float timePassed = 0f;

        while (sr.size.x < laserRange)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime;

            cc.offset = new Vector2(Mathf.Lerp(1f, laserRange, linearT) / 2, cc.offset.y);
            cc.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), cc.size.y);



            sr.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);
            yield return null;
        }
        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());

    }



}
