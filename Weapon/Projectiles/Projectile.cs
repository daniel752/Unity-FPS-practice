using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float projectileSpeed = 200;
    protected float damage;
    protected float range = 70;
    protected Vector3 initialPosition;
    [SerializeField] protected GameObject impactEffect;

    protected virtual void Awake()
    {
        initialPosition = transform.position;
        Debug.DrawRay(initialPosition, Vector3.up * 0.5f, Color.red, 5.0f);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * projectileSpeed, ForceMode.Impulse);
        Debug.DrawLine(initialPosition, transform.forward * range, Color.magenta, 2f);
    }

    protected void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log($"{this} collided with enemy and did damage of {damage}");
            GameObject enemyObj = other.gameObject;
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if (enemy.GetTarget() == null)
                enemy.SetTarget(GameObject.FindWithTag("Player"));
            EnemyHealth enemyHealth = enemyObj.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
            enemyHealth.healthBar.value -= damage;

            if (enemyHealth.GetHealth() <= 0)
            {
                enemyHealth.SetHealth(0);
                Destroy(enemyObj.gameObject);
                Instantiate(enemyHealth.dyingEffect,enemyObj.transform.position,enemyObj.transform.rotation);
                enemyObj.GetComponent<Enemy>().Die();
            }
        }
        // else if (other.gameObject.tag == "Player")
        // {
        //     Debug.Log($"{this} collided with player and did damage of {damage}");
        //     GameObject playerObj = other.gameObject;
        //     PlayerHealth playerHealth = playerObj.GetComponent<PlayerHealth>();
        //     playerHealth.TakeDamage(damage);
            
        // }

        Instantiate(impactEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);   
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public float GetDamage()
    {
        return damage;
    }
    public float GetRange()
    {
        return range;
    }
    public void SetRange(float range)
    {
        this.range = range;
    }
}
