using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] float movespeed = 2f;

    SpriteRenderer sr;
    private Rigidbody2D rb;
    private Vector2 moveDir;
    KnockBack kn;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        kn = GetComponent<KnockBack>();
    }

    private void FixedUpdate()
    {
        if (kn.gettingKnockedBack)
        {
            return;
        }
        rb.MovePosition(rb.position + moveDir * (movespeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
        if (moveDir.x < 0)
        {
            sr.flipX = true;
        }
        else if (moveDir.x > 0)
        {
            sr.flipX = false;
        }
    }
    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }


}
