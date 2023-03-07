using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    // public static InventoryManager instance;
    public int inventorySize = 20;
    public GameObject slotPrefab;
    public GameObject itemInfoPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public float slowTimeEffect = 0f;
    private int normalTime = 1;
    // public Inventory inventory;

    public void Init()
    {
        // if (instance == null)
        // {
        //     instance = this;
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
        
        for (int i = 0; i < inventorySize; i++)
        {
            // Debug.Log($"Init inventory slot {i}");
            CreateInventorySlot();
        }
    }

    // private void Start()
    // {
    //     for (int i = 0; i < inventorySize; i++)
    //     {
    //         Debug.Log($"Init inventory slot {i}");
    //         CreateInventorySlot();
    //     }
    // }

    public void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab, transform);
        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();
        inventorySlots.Add(newSlotComponent);
        // newSlotComponent.GetComponent<Image>().enabled = false;
        // newSlotComponent.GetComponent<TextMeshProUGUI>().enabled = false;
        // newSlotComponent.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    public void OpenInventory()
    {
        // Transform mainCamera = GameObject.FindWithTag("MainCamera").gameObject.transform;
        // mainCamera.SetParent(GameObject.FindWithTag("InventoryUI").transform);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.transform.parent.gameObject.SetActive(true);
        Time.timeScale = slowTimeEffect;
    }
    public void CloseInventory()
    {
        // Transform mainCamera = GameObject.FindWithTag("MainCamera").gameObject.transform;
        // mainCamera.SetParent(GameObject.FindWithTag("Player").transform);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.transform.parent.gameObject.SetActive(false);
        // GameObject inventoryUI = GameObject.Find("InventoryUI");
        // inventoryUI.SetActive(false);
        // gameObject.SetActive(false);
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
}