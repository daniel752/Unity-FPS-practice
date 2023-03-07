using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI stackSizeText;
    private InventoryItem inventoryItem;
    // private GameObject itemInfoPanel;
    // private TextMeshProUGUI item

    public void DrawSlot(InventoryItem item)
    {
        if (item.item != null)
        {
            icon.sprite = item.item.icon;
            icon.enabled = true;
            itemName.text = item.item.itemName;
            if (item.stackSize == 1)
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
        Debug.Log("Mouse clicked");
        if (inventoryItem.item.itemType == ItemType.Consumable)
        {
            inventoryItem.item.Use();
        }
        else if (inventoryItem.item.itemType == ItemType.Weapon)
        {
            Debug.Log($"Clicked slot {inventoryItem.item.itemName}");
            WeaponItem weaponItem = inventoryItem.item as WeaponItem;
            WeaponManager weaponManager = GameObject.FindWithTag("Player").GetComponent<WeaponManager>();
            weaponManager.EquipWeapon(weaponItem);
            Inventory inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
            inventory.EquipItem(weaponItem);
            inventory.RemoveItem(weaponItem);
            ClearSlot();
        }
    }
    public void OnMouseOver()
    {
        Debug.Log($"Mouse over slot {inventoryItem.item.itemName}");
        GameObject itemInfoPanel = GameObject.FindWithTag("ItemInfoPanel");
        Transform itemName = itemInfoPanel.transform.Find("ItemName");
        Transform itemDescription = itemInfoPanel.transform.Find("ItemDescription");
        itemName.GetComponent<TextMeshProUGUI>().text = inventoryItem.item.itemName;
        itemDescription.GetComponent<TextMeshProUGUI>().text = inventoryItem.item.itemDescription;
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