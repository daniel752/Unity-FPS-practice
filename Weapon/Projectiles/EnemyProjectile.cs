using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void OnCollisionEnter(Collision other)
    {
        Debug.Log($"{this} collided with {other.gameObject.name}");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log($"{this} collided with player and did damage of {damage}");
            GameObject playerObj = other.gameObject;
            PlayerHealth playerHealth = playerObj.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
            
        }

        Instantiate(impactEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);   
    }
}
