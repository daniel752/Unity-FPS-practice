using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : HealthSystem
{
    public Slider healthBar;
    public GameObject HealthBarUI;
    public GameObject dyingEffect;

    void Awake()
    {
        health = maxHealth;
        healthBar = GetComponentInChildren<Slider>();
    }
}
