using UnityEngine;

public enum ItemType
{
    Consumable,
    Weapon,
    Equipment,
    Quest,
    Misc
}

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool stackable;
    public ItemType itemType;
    public int amount;
    public bool equipable;
    [TextArea(3,5)]
    public string itemDescription;

    public virtual void Use()
    {
        Debug.Log($"Using item {this}");
    }
}