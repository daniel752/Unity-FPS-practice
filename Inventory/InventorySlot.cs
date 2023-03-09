using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI stackSizeText;
    private InventoryItem inventoryItem;

    public void DrawSlot(InventoryItem item)
    {
        if (item.item != null)
        {
            icon.sprite = item.item.icon;
            icon.enabled = true;
            itemName.text = item.item.itemName;
            if (item.stackSize <= 1)
                stackSizeText.text = "";
            else
                stackSizeText.text = item.stackSize.ToString();
        }
        else
        {
            icon.sprite = item.weaponItem.icon;
            icon.enabled = true;
            itemName.text = item.weaponItem.itemName;
            stackSizeText.text = "";
        }
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        itemName.text = "";
        stackSizeText.text = "";
    }

    public void SetSlotItem(InventoryItem inventoryItem)
    {
        this.inventoryItem = inventoryItem;
    }

    public void UnSetSlotItem()
    {
        inventoryItem = null;
    }

    public void OnClick()
    {
        if (Mouse.current.clickCount.ReadValue() == 2)
        {
            Inventory inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
            
            if (inventoryItem.item is ConsumableItem)
            {
                // Debug.Log("item clicked");
                ConsumableItem consumableItem = (ConsumableItem)inventoryItem.item;
                HealthSystem playerHealth = GameObject.FindWithTag("Player").GetComponent<HealthSystem>();
                consumableItem.Use(playerHealth);
                inventory.RemoveItem(consumableItem);
            }
            else if (inventoryItem.item is WeaponItem)
            {
                // Debug.Log("equip item");
                WeaponItem weaponItem = (WeaponItem)inventoryItem.item;
                WeaponManager weaponManager = GameObject.FindWithTag("Player").GetComponent<WeaponManager>();
                weaponManager.EquipWeapon(weaponItem);
                if (!inventoryItem.equipped)
                {
                    // Debug.Log("moving item to equipment");
                    // inventoryItem.equipped = true;
                    inventory.GetEquipment().EquipItem(inventoryItem);
                    inventory.RemoveItem(weaponItem);
                    ClearSlot();
                }
            }
        }
    }
    public void OnMouseOver()
    {
        if (inventoryItem.weaponItem != null)
        {
            // Debug.Log($"Mouse over slot {inventoryItem.weaponItem.itemName}");
            GameObject itemInfoPanel = GameObject.FindWithTag("ItemInfoPanel");
            Transform itemName = itemInfoPanel.transform.Find("ItemName");
            Transform itemDescription = itemInfoPanel.transform.Find("ItemDescription");
            itemName.GetComponent<TextMeshProUGUI>().text = inventoryItem.weaponItem.itemName;
            itemDescription.GetComponent<TextMeshProUGUI>().text = inventoryItem.weaponItem.itemDescription;
        }
        else
        {
            // Debug.Log($"Mouse over slot {inventoryItem.item.itemName}");
            GameObject itemInfoPanel = GameObject.FindWithTag("ItemInfoPanel");
            Transform itemName = itemInfoPanel.transform.Find("ItemName");
            Transform itemDescription = itemInfoPanel.transform.Find("ItemDescription");
            itemName.GetComponent<TextMeshProUGUI>().text = inventoryItem.item.itemName;
            itemDescription.GetComponent<TextMeshProUGUI>().text = inventoryItem.item.itemDescription;    
        }
    }
    
    // public void OnMouseExit() 
    // {
    //     Debug.Log("Mouse out");
    //     GameObject itemInfoPanel = GameObject.FindWithTag("ItemInfoPanel");
    //     Transform itemName = itemInfoPanel.transform.Find("ItemName");
    //     Transform itemDescription = itemInfoPanel.transform.Find("ItemDescription");
    //     itemName.GetComponent<TextMeshProUGUI>().text = "";
    //     itemDescription.GetComponent<TextMeshProUGUI>().text = "";
    // }
}