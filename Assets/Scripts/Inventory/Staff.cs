using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform laserSpawnPoint;
    Animator animator;
    readonly int FIRE_HASH = Animator.StringToHash("Fire");
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        MouseFollowWithOffSet();
    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    public void Attack()
    {
        animator.SetTrigger(FIRE_HASH);

    }
    void MouseFollowWithOffSet()
    {
        Vector3 pozycjaMyszki = Input.mousePosition;
        Vector3 pozycjaGracza = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(pozycjaMyszki.y, pozycjaMyszki.x) * Mathf.Rad2Deg;

        if (pozycjaMyszki.x < pozycjaGracza.x)
        {
            ActiveWeapons.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapons.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    void SpawnStaffProjectileAnimEvent()
    {
        GameObject newMagicLaser = Instantiate(magicLaser, laserSpawnPoint.position, Quaternion.identity);
        newMagicLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }









}
