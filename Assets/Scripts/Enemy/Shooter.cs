using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed = 1;
    [SerializeField] private int bustCount;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    private bool isShooting = false;
    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }
    private IEnumerator ShootRoutine()
    {
        for (int i = 0; i < bustCount; i++)
        {
            isShooting = true;
            Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.transform.right = targetDirection;

            if (newBullet.TryGetComponent(out Arrow arrow))
            {
                arrow.UpdateMoveSpeed(bulletMoveSpeed);
            }
            yield return new WaitForSeconds(timeBetweenBursts);
        }


        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }
}
