using System;

[Serializable]
public class InventoryItem
{
    public Item item;
    public int stackSize;

    public InventoryItem(Item item)
    {
        this.item = item;
        // stackSize = 1;
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