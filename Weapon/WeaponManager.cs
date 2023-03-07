using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponSlot;
    [SerializeField] GameObject currentWeapon;
    [SerializeField] WeaponItem weaponItem;
    public delegate void WeaponEquippedHandler(Weapon weapon);
    public static WeaponEquippedHandler OnWeaponEquipped;

    public void EquipWeapon(WeaponItem weaponItem)
    {
        this.weaponItem = weaponItem;
        if (currentWeapon != null)
        {
            Debug.Log($"Destroying {currentWeapon}");
            Destroy(currentWeapon);
        }

        Debug.Log($"Creating new weapon {weaponItem.itemName} with dmg {weaponItem.damage} and fire rate {weaponItem.fireRate}");
        currentWeapon = Instantiate(weaponItem.weaponPrefab,weaponSlot);
        Weapon weapon = currentWeapon.GetComponent<Weapon>();
        weapon.SetWeapon(weaponItem);
        weapon.Init();
        OnWeaponEquipped?.Invoke(weapon);

        //Maybe need to set the local position and rotation
    }
    public GameObject GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
