using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;
    private PlayerControls playerControls;
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void Start()
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        ToggleActiveHighlight(0);
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue);
    }

    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
        ChangeActiveWeapon();
    }
    private void ChangeActiveWeapon()
    {
        if (ActiveWeapons.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapons.Instance.CurrentActiveWeapon.gameObject);
        }
        if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>())
        {
            ActiveWeapons.Instance.WeaponNull();
            return;
        }
        GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapons.Instance.transform.position, ActiveWeapons.Instance.transform.rotation);

        newWeapon.transform.parent = ActiveWeapons.Instance.transform;
        ActiveWeapons.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }



}
