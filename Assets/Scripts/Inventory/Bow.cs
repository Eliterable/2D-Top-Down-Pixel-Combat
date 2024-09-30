using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    private Animator animator;
    readonly int FIRE_HASH = Animator.StringToHash("Fire");
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Attack()
    {
        animator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapons.Instance.transform.rotation);
        newArrow.GetComponent<Arrow>().UpdateWeaponInfo(weaponInfo);

    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
