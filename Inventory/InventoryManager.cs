using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    // public static InventoryManager instance;
    public int inventorySize = 20;
    public int equipmentSize = 8;
    public GameObject slotPrefab;
    public GameObject itemInfoPrefab;
    public GameObject equipmentPrefab;
    public GameObject equipmentSlotPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public List<InventorySlot> equipmentSlots = new List<InventorySlot>();
    public float slowTimeEffect = 0f;
    private int normalTime = 1;

    public void Init()
    {   
        for (int i = 0; i < inventorySize; i++)
        {
            // Debug.Log($"Init inventory slot {i}");
            CreateInventorySlot();
        }
        for (int i = 0; i < equipmentSize; i++)
        {
            CreateEquipmentSlot(equipmentPrefab.transform);
        }
    }

    public void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab, transform);
        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();
        inventorySlots.Add(newSlotComponent);
    }
    public void CreateEquipmentSlot(Transform transform)
    {
        GameObject newSlot = Instantiate(equipmentSlotPrefab, transform);
        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();
        equipmentSlots.Add(newSlotComponent);
    }

    public void OpenInventory()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.transform.parent.gameObject.SetActive(true);
        Time.timeScale = slowTimeEffect;
    }
    public void CloseInventory()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.transform.parent.gameObject.SetActive(false);
        Time.timeScale = normalTime;
    }

    public void UpdateInventoryUI(List<InventoryItem> inventoryItems)
    {
        // Debug.Log($"Count:{inventorySlots.Count}");
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            // Debug.Log("Entering loop");
            if (i < inventoryItems.Count)
            {
                // Debug.Log("Drawing slot");
                inventorySlots[i].SetSlotItem(inventoryItems[i]);
                inventorySlots[i].DrawSlot(inventoryItems[i]);
            }
            else
            {
                inventorySlots[i].UnSetSlotItem();
                inventorySlots[i].ClearSlot();
            }
        }
    }
    public void UpdateEquipmentUI(List<InventoryItem> equipmentItems)
    {
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            Debug.Log($"number of equipment items: {equipmentItems.Count}");
            if (i < equipmentItems.Count)
            {
                Debug.Log($"item props: icon - {equipmentItems[i].weaponItem.icon}");
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