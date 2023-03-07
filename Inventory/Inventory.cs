using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventorySize = 20;
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    public List<Item> items = new List<Item>();
    public GameObject weaponSlot;
    private Weapon weapon;
    [SerializeField] private InventoryManager inventoryManager;
    // public Dictionary<Item, InventoryItem> itemDictionary = new Dictionary<Item, InventoryItem>();

    public void AddItem(Item item)
    {
        // if (item.stackable && itemDictionary.TryGetValue(item, out InventoryItem itemSlot))
        // inventoryItems.Find(i => i.item.itemName == item.itemName);
        
        if (item.stackable && items.Contains(item))
        {
            InventoryItem tempItem = inventoryItems.Find(i => i.item.itemName == item.itemName); 
            tempItem.AddToStack(item.amount);
            // inventoryItems.Find(i => i.item.itemName == item.itemName).AddToStack();
            items.Add(item);
            // inventoryItems.AddToStack();
            inventoryManager.UpdateInventoryUI(inventoryItems);
            // Debug.Log($"{item.itemName} has been added to stack, stack now is {inventoryItems.Find(i => i.item.itemName == item.itemName).stackSize}");
        }
        else
        {
            if (inventoryItems.Count < inventorySize)
            {
                InventoryItem newItemSlot = new InventoryItem(item);
                newItemSlot.AddToStack(item.amount);
                items.Add(item);
                inventoryItems.Add(newItemSlot);
                // itemDictionary.Add(item, newItemSlot);
                inventoryManager.UpdateInventoryUI(inventoryItems);
                // Debug.Log($"{newItemSlot.item.itemName} has been added to inventory");
            }
            else
            {
                Debug.Log("Inventory is full");
            }
        }
    }

    public void RemoveItem(Item item)
    {
        // if (itemDictionary.TryGetValue(item, out InventoryItem itemSlot))
        // itemSlot.RemoveFromStack();
        InventoryItem tempItem = inventoryItems.Find(i => i.item.itemName == item.itemName);
        if (tempItem != null)
        {
            tempItem.RemoveFromStack();
            if (tempItem.stackSize == 0)
            {
                inventoryItems.Remove(tempItem);
                items.Remove(tempItem.item);
                inventoryManager.UpdateInventoryUI(inventoryItems);
            }
        }
    }
    public void RemoveItem(WeaponItem weaponItem)
    {
        Debug.Log("Removing weapon item");
        // InventoryItem itemToRemove = null;
        
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

    // public void EquipWeaponItem(WeaponItem weaponItem)
    // {
    //     weapon.SetWeapon(weaponItem.weaponPrefab);
    // }
}