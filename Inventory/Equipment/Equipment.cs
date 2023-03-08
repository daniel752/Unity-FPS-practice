using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] List<IItem> items = new List<IItem>();
    [SerializeField] List<InventoryItem> equipmentItems = new List<InventoryItem>();
    [SerializeField] EquipmentManager equipmentManager;

    // public void AddItem(IItem itemToAdd)
    // {
    //     InventoryItem newEquipmentItem = new InventoryItem(itemToAdd);
    //     equipmentItems.Add(newEquipmentItem);
    //     equipmentManager.UpdateEquipmentUI(equipmentItems);
    // }
    public void RemoveItem(InventoryItem itemToRemove)
    {
        InventoryItem equipmentItemToRemove = new InventoryItem(itemToRemove.item);
        equipmentItemToRemove.equipped = false;
        equipmentItems.Remove(equipmentItemToRemove);
        equipmentManager.UpdateEquipmentUI(equipmentItems);
    }
    public void EquipItem(InventoryItem item)
    {
        InventoryItem itemToEquip = new InventoryItem(item.item);
        itemToEquip.equipped = true;
        equipmentItems.Add(itemToEquip);
        equipmentManager.UpdateEquipmentUI(equipmentItems);
    }
}
