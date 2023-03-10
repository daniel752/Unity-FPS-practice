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

        currentWeapon = Instantiate(weaponItem.itemPrefab,weaponSlot);
        Weapon weapon = currentWeapon.GetComponent<Weapon>();
        Debug.Log($"Equipped weapon {weaponItem.itemName} with dmg {weaponItem.GetDamage()} and fire rate {weaponItem.GetFireRate()} with magazine size {weaponItem.GetMagazineSize()}");
        weapon.SetWeapon(weaponItem);
        weapon.Init();
        OnWeaponEquipped?.Invoke(weapon);
        // GetComponent<DisplayAmmoUI>().Init();

        //Maybe need to set the local position and rotation
    }
    public GameObject GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
