using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] Item item;

    protected override void Interact()
    {
        Inventory inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();

        if (item is MiscItem)
        {
            MiscItem miscItem = (MiscItem)item;
            inventory.AddItem(miscItem);
        }
        else if (item is ConsumableItem)
        {
            ConsumableItem consumableItem = (ConsumableItem)item;
            inventory.AddItem(consumableItem);
        }
        else if (item is WeaponItem)
        {
            WeaponItem weaponItem = (WeaponItem)item;
            inventory.AddItem(weaponItem);
        }

        // inventory.AddItem(item);
        // if (inventory.AddItem(item))
            // Debug.Log("Item added to inventory");
        // Debug.Log("Picking up " + item.name);
        Destroy(gameObject);
    }

    public Item GetItem()
    {
        return item;
    }
}
