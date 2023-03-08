using System;

[Serializable]
public class InventoryItem
{
    public IItem item;
    // public Item item;
    public WeaponItem weaponItem;
    public int stackSize;
    public bool equipped;

    public InventoryItem(IItem item)
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