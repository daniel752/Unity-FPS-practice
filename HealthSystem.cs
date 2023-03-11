using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100f;
    protected float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }
    public virtual void RestoreHealth(float heal)
    {
        health += heal;
    }

    public float GetHealth()
    {
        return health;
    }
    public void SetHealth(float health)
    {
        this.health = health;
    }
}
