using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float knockBackThrustAmount = 7f;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float damageReconveryTime = 1f;
    private int currentHealth;
    private KnockBack knockBack;
    private Flash flash;
    private bool canTakeDamage = true;
    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, other.transform);

        }
    }
    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage)
        {
            return;
        }
        knockBack.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;

        StartCoroutine(DamageRecoveryRoutine());

    }
    IEnumerator DamageRecoveryRoutine()
    {

        yield return new WaitForSeconds(damageReconveryTime);
        canTakeDamage = true;
    }
}
