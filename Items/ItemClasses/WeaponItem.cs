using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Item/Weapon Item")]
public class WeaponItem : Item
{
    [SerializeField] int damage;
    [SerializeField] int fireRate;
    [SerializeField] int range;
    [SerializeField] int magazineSize;
    // [SerializeField] GameObject weaponPrefab;

    public int GetDamage()
    {
        return damage;
    }
    public int GetFireRate()
    {
        return fireRate;
    }
    public int GetMagazineSize()
    {
        return magazineSize;
    }
    public int GetRange()
    {
        return range;
    }
    // public GameObject GetWeaponPrefab()
    // {
    //     return weaponPrefab;
    // }
}
