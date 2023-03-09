using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 500f;
    private float damage;
    public float range = 100f;
    private Vector3 initialPosition;
    public GameObject impactEffect;

    void Awake()
    {
        initialPosition = transform.position;
        Debug.DrawRay(initialPosition, Vector3.up * 0.5f, Color.red, 5.0f);
        Rigidbody rb = GetComponent<Rigidbody>();
        Camera mainCamera = Camera.main;
        Vector3 centerOfScreen = new Vector3(mainCamera.pixelWidth / 2.0f, mainCamera.pixelHeight / 2.0f, 0);
        Vector3 direction = mainCamera.ScreenToWorldPoint(centerOfScreen) - transform.position;
        rb.AddRelativeForce(Vector3.forward * projectileSpeed,ForceMode.Impulse);
    }

    void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log($"{this} collided with enemy and did damage of {damage}");
            GameObject enemy = other.gameObject;
        }

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
}
