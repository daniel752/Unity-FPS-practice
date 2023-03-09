using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Item/Weapon Item")]
public class WeaponItem : Item
{
    public int damage;
    public int fireRate;
    public GameObject weaponPrefab;
}
