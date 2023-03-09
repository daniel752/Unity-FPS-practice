using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType
{
    health,
    mana,
    buff
}

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Item/Consumable Item")]
public class ConsumableItem : Item
{
    [SerializeField] ConsumableType type;
    [SerializeField] int potency;

    public void Use(HealthSystem health)
    {
        if (health.GetHealth() < 100f)
            health.RestoreHealth(potency);
    }
}
