using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public int equipmentSize = 5;
    public GameObject equipmentPrefab;
    public GameObject equipmentSlotPrefab;
    public List<InventorySlot> equipmentSlots = new List<InventorySlot>();

    public void Init()
    {
        for (int i = 0; i < equipmentSize; i++)
        {
            CreateEquipmentSlot(equipmentPrefab.transform);
        }
    }

    public void CreateEquipmentSlot(Transform transform)
    {
        GameObject newSlot = Instantiate(equipmentSlotPrefab, transform);
        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();
        equipmentSlots.Add(newSlotComponent);
    }

    public void UpdateEquipmentUI(List<InventoryItem> equipmentItems)
    {
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            // Debug.Log($"number of equipment items: {equipmentItems.Count}");
            if (i < equipmentItems.Count)
            {
                // Debug.Log($"item props: icon - {equipmentItems[i].weaponItem.icon}");
                equipmentSlots[i].SetSlotItem(equipmentItems[i]);
                equipmentSlots[i].DrawSlot(equipmentItems[i]);
            }
            else
            {
                equipmentSlots[i].UnSetSlotItem();
                equipmentSlots[i].ClearSlot();
            }
        }
    }
}
