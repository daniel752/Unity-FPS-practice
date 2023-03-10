using System;

[Serializable]
public class InventoryItem
{
    public Item item;
    public int stackSize;
    public bool equipped;

    public InventoryItem(Item item)
    {
        this.item = item;
        equipped = false;
        // stackSize = 1;
    }
    // public InventoryItem(WeaponItem weaponItem)
    // {
    //     this.weaponItem = weaponItem;
    // }

    public void AddToStack(int amount)
    {
        // stackSize++;
        stackSize += amount;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}