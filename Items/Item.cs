using UnityEngine;

public enum ItemType
{
    Consumable,
    Weapon,
    Equipment,
    Quest,
    Misc
}

public interface IItem
{
    string itemName { get; }
    Sprite icon { get; }
    bool stackable { get; }
    ItemType itemType { get; }
    int amount { get; }
    bool equipable { get; }
    string itemDescription { get; }
    void Use();
}

[CreateAssetMenu]
public class Item : ScriptableObject, IItem
{
    public string itemName;
    public Sprite icon;
    public bool stackable;
    public ItemType itemType;
    public int amount;
    public bool equipable;
    [TextArea(3,5)]
    public string itemDescription;

    string IItem.itemName { get { return itemName; } }

    Sprite IItem.icon { get { return icon; } }

    bool IItem.stackable { get { return stackable; } }

    ItemType IItem.itemType { get { return itemType;} }

    int IItem.amount { get { return amount;} }

    bool IItem.equipable { get { return equipable;} }

    string IItem.itemDescription { get { return itemDescription;} }

    public virtual void Use()
    {
        Debug.Log($"Using item {this}");
    }
}