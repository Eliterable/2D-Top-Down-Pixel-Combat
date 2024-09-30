using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startinghealth = 100;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float mocKnockBacku = 15f;
    private int currentHealth;
    KnockBack kn;
    Flash flash;
    GameObject slimeDeathVFX;


    private void Awake()
    {
        flash = GetComponent<Flash>();
        kn = GetComponent<KnockBack>();
    }
    private void Start()
    {
        currentHealth = startinghealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        kn.GetKnockedBack(PlayerController.Instance.transform, mocKnockBacku);
        StartCoroutine(flash.FlashRoutine());

        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            slimeDeathVFX = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, .1f);
        }
    }



}
