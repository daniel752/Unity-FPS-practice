using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemPickup : Interactable
{
    [SerializeField] WeaponItem weaponItem;
    protected override void Interact()
    {
        Inventory inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        inventory.AddItem(weaponItem);
        // if (inventory.AddItem(item))
            // Debug.Log("Item added to inventory");
        // Debug.Log("Picking up " + item.name);
        Destroy(gameObject);
    }
}
