using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject slash;
    [SerializeField] Transform slashpos;

    //[SerializeField] private float attackCD = 0.1f;
    [SerializeField] private WeaponInfo weaponInfo;
    Transform weaponColider;
    private Animator animator;

    private void Start()
    {
        slashpos = GameObject.Find("SlashPos").transform;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        weaponColider = PlayerController.Instance.GetWeaponCollider();
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
        animator.SetTrigger("Attack");
        weaponColider.gameObject.SetActive(true);
        InstantiateSlash();
    }


    private void DoneAttackingAnimEvent()
    {
        weaponColider.gameObject.SetActive(false);
    }
    void MouseFollowWithOffSet()
    {
        Vector3 pozycjaMyszki = Input.mousePosition;
        Vector3 pozycjaGracza = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(pozycjaMyszki.y, pozycjaMyszki.x) * Mathf.Rad2Deg;



        if (pozycjaMyszki.x < pozycjaGracza.x)
        {
            ActiveWeapons.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponColider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapons.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponColider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void InstantiateSlash()
    {
        GameObject slashAnimation;
        slashAnimation = Instantiate(slash, slashpos.transform.position, Quaternion.identity);
        slashAnimation.transform.parent = this.transform.parent;
        if (PlayerController.Instance.FaceingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }

    }



}
