using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private GameObject particleOnHitVFX;
    private WeaponInfo weaponInfo;
    private Vector3 startPosition;
    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        ArrowMovement();
        DetectFireDistance();
    }
    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        if (!other.isTrigger && (enemyHealth || indestructible))
        {

            Instantiate(particleOnHitVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);
        }
    }

    void ArrowMovement()
    {
        transform.Translate(1 * Time.deltaTime * moveSpeed, 0, 0);
    }

}
