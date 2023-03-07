using System;

[Serializable]
public class InventoryItem
{
    public Item item;
    public WeaponItem weaponItem;
    public int stackSize;

    public InventoryItem(Item item)
    {
        this.item = item;
        // stackSize = 1;
    }
    public InventoryItem(WeaponItem weaponItem)
    {
        this.weaponItem = weaponItem;
    }

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