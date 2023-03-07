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

    // Update is called once per frame
    void Update()
    {
        // healthBar.value = health;
        // if(takeDamage)
        //     TakeDamage(20);
        // health = Mathf.Clamp(health,0,maxHealth);
        // UpdateHealthUI();
    }
    private void OnCollisionEnter(Collision other) 
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if(projectile != null)
        {
            TakeDamage(projectile.GetDamage());
            healthBar.value -= projectile.GetDamage();
            // Debug.Log(gameObject.name + " health: " + health);
            if(health <= 0)
            {
                health = 0;
                Destroy(gameObject);
                Instantiate(dyingEffect,transform.position,transform.rotation);
            }
        }
    }
}
