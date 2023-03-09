using UnityEngine;
using System;

public enum ItemType
{
    Consumable,
    Weapon,
    Equipment,
    Quest,
    Misc
}

// public interface IItem
// {
//     string itemName { get; }
//     Sprite icon { get; }
//     bool stackable { get; }
//     ItemType itemType { get; }
//     int amount { get; }
//     bool equipable { get; }
//     string itemDescription { get; }
//     void Use();
// }

[CreateAssetMenu]
public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool stackable;
    public ItemType itemType;
    public int amount;
    public bool equipable;
    [TextArea(3,5)]
    public string itemDescription;

    public string GetitemName()
    { return itemName; }
    public Sprite Geticon()
    { return icon; }
    public bool Getstackable()
    { return stackable; }
    public ItemType GetitemType()
    { return itemType; }
    private int Getamount()
    { return amount; }
    private bool Getequipable()
    { return equipable; }
    private string GetitemDescription()
    { return itemDescription; }
}