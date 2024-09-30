using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] float knockBackTime = 0.5f;
    public bool gettingKnockedBack { get; private set; }
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        gettingKnockedBack = true;
        Vector2 diffrence = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(diffrence, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }
    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }

}
