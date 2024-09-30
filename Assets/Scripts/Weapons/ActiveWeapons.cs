using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapons : Singleton<ActiveWeapons>
{
    public MonoBehaviour CurrentActiveWeapon
    {
        get;
        private set;

    }
    private float timeBetweenAttacks;
    private PlayerControls playerControls;
    private bool attackButtonDown, isAttacking = false;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }
    private void Update()
    {
        Attack();
    }
    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }


    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
        AttackCooldown();
    }
    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());

    }
    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }
    private void StartAttacking()
    {
        attackButtonDown = true;
    }
    private void StopAttacking()
    {
        attackButtonDown = false;
    }
    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();
        }

    }
    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }
}
