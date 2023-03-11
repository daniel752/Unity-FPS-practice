using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayAmmoUI : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI ammoText;
    // Weapon weapon;

    public void Init(Weapon weapon) 
    {
        // Debug.Log($"Init ammo UI");
        // weapon = transform.Find("WeaponSlot").GetComponentInChildren<Weapon>();
        UpdateAmmoUI(weapon);
    }
    public void UpdateAmmoUI(Weapon weapon)
    {
        // Debug.Log($"ammo left {weapon.GetBulletsLeft()} / {weapon.GetMagazineSize()}");
        ammoText.text = weapon.GetBulletsLeft() + " / " + weapon.GetMagazineSize();
    }
}
