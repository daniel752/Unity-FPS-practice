﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Interactable
{
    [SerializeField] List<Item> items = new List<Item>();
    AudioSource pickSfx;

    private void Awake() 
    {
        pickSfx = GetComponentInChildren<AudioSource>();

        //Inactivate all objects stored in this game object
        GameObject itemsObj = transform.Find("Items").gameObject;
    }

    protected override void Interact()
    {
        pickSfx.Play();
        int numItems = transform.Find("Items").childCount;
        Debug.Log($"number of items:{numItems}");
        GameObject itemsObj = transform.Find("Items").gameObject;
        for (int i = 0; i < numItems; i++)
        {
            items.Add(itemsObj.transform.GetComponentsInChildren<ItemPickup>()[i].GetItem());
        }
        Inventory inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        inventory.AddItems(items);
        // if (inventory.AddItem(item))
            // Debug.Log("Item added to inventory");
        // Debug.Log("Picking up " + item.name);
        Destroy(gameObject);
    }
}
