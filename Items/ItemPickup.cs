using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] Item item;

    protected override void Interact()
    {
        Inventory inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        inventory.AddItem(item);
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
