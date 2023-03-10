using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventorySize = 20;
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    public List<Item> items = new List<Item>();
    // List<InventoryItem> equipmentItems = new List<InventoryItem>();
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] Equipment equipment;
    int money;
    private void Awake() 
    {
        equipment = GetComponent<Equipment>();    
    }

    public void AddItems(List<Item> itemsToAdd)
    {
        foreach (Item itemToAdd in itemsToAdd)
        {
            if (itemToAdd.itemName == "Money")
            {
                AddMoney(itemToAdd.amount);
            }
            else if (itemToAdd.stackable && items.Contains(itemToAdd))
            {
                InventoryItem newItemSlot = inventoryItems.Find(i => i.item.itemName == itemToAdd.itemName); 
                newItemSlot.AddToStack(itemToAdd.amount);
                items.Add(itemToAdd);
                inventoryManager.UpdateInventoryUI(inventoryItems);
                // Debug.Log($"{item.itemName} has been added to stack, stack now is {inventoryItems.Find(i => i.item.itemName == item.itemName).stackSize}");
            }
            else
            {
                if (inventoryItems.Count < inventorySize)
                {
                    InventoryItem newItemSlot = new InventoryItem(itemToAdd);
                    newItemSlot.AddToStack(itemToAdd.amount);
                    items.Add(itemToAdd);
                    inventoryItems.Add(newItemSlot);
                    inventoryManager.UpdateInventoryUI(inventoryItems);
                    // Debug.Log($"{newItemSlot.item.itemName} has been added to inventory");
                }
            }
        }
    }

    private void AddMoney(int amount)
    {
        money += amount;
        inventoryManager.UpdateMoneyUI(money);
    }

    public void AddItem(Item itemToAdd)
    {   
        // Debug.Log($"Item name: {itemToAdd.itemName}");
        if (itemToAdd.itemName == "Money")
        {
            // Debug.Log("Adding money");
            AddMoney(itemToAdd.amount);
        }
        else if (itemToAdd.stackable && items.Contains(itemToAdd))
        {
            InventoryItem newItemSlot = inventoryItems.Find(i => i.item.itemName == itemToAdd.itemName); 
            newItemSlot.AddToStack(itemToAdd.amount);
            items.Add(itemToAdd);
            inventoryManager.UpdateInventoryUI(inventoryItems);
            // Debug.Log($"{item.itemName} has been added to stack, stack now is {inventoryItems.Find(i => i.item.itemName == item.itemName).stackSize}");
        }
        else
        {
            if (inventoryItems.Count < inventorySize)
            {
                InventoryItem newItemSlot = new InventoryItem(itemToAdd);
                newItemSlot.AddToStack(itemToAdd.amount);
                items.Add(itemToAdd);
                inventoryItems.Add(newItemSlot);
                inventoryManager.UpdateInventoryUI(inventoryItems);
                // Debug.Log($"{newItemSlot.item.itemName} has been added to inventory");
            }
            else
            {
                // Debug.Log("Inventory is full");
            }
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        InventoryItem itemSlotToRemove = inventoryItems.Find(i => i.item.itemName == itemToRemove.itemName);
        if (itemSlotToRemove != null)
        {
            itemSlotToRemove.RemoveFromStack();
            if (itemSlotToRemove.stackSize == 0)
            {
                inventoryItems.Remove(itemSlotToRemove);
                items.Remove(itemSlotToRemove.item);
                inventoryManager.UpdateInventoryUI(inventoryItems);
            }
        }
    }
    public void RemoveItem(WeaponItem weaponItem)
    {
        // Debug.Log("Removing weapon item");
        
        foreach (InventoryItem item in inventoryItems)
        {
            if (item.item is WeaponItem && item.item.itemName == weaponItem.itemName)
            {
                item.RemoveFromStack();
                if (item.stackSize == 0)
                {
                    inventoryItems.Remove(item);
                    items.Remove(item.item);
                }
                inventoryManager.UpdateInventoryUI(inventoryItems);
            }
        }
    }
    public Equipment GetEquipment()
    {
        return equipment;
    }
}