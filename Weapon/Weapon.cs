using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] WeaponItem weaponItem;
    Transform spawnPosition;
    int fireRate;
    int damage;
    float timeBetweenShots;
    public GameObject muzzleFlash;
    public bool firing;
    // PlayerMotor playerMotor;
    public void Init()
    {
        // Debug.Log($"weapon damage:{weaponItem.damage}, weapon fire rate:{weaponItem.fireRate}");
        SetWeaponStats();
        SetSpawnPosition();
    }

    private void SetSpawnPosition()
    {
        spawnPosition = transform.Find("WeaponTip").transform;
    }

    // private void Start()
    // {
    //     // playerMotor = GameObject.FindWithTag("Player").GetComponent<PlayerMotor>();
    //     Debug.Log($"weapon damage:{weaponItem.damage}, weapon fire rate:{weaponItem.fireRate}");
    //     SetWeaponStats();
    // }

    private void SetWeaponStats()
    {
        damage = weaponItem.damage;
        fireRate = weaponItem.fireRate;
    }

    private void LateUpdate()
    {
        timeBetweenShots += Time.deltaTime;

        if(firing && timeBetweenShots >= 1f / fireRate)
        {
            Fire();
        }
    }
    public void Fire()
    {
        // Debug.Log($"fire rate:{fireRate}");
        // Debug.Log($"time:{timeBetweenShots} >=? {1f / fireRate}");
        if(firing && timeBetweenShots >= 1f / fireRate)
        {
            // Debug.Log("Bang bang");
            //Getting crosshair position
            Vector3 spawnDirection = GetSpawnDirection();
            //Shooting bullets
            Projectile projectileObj = Instantiate(projectilePrefab,spawnPosition.position,Quaternion.LookRotation(spawnDirection)).GetComponent<Projectile>();
            projectileObj.SetDamage(damage);

            //Muzzle flash effect for gun
            GameObject newMuzzleFlash = Instantiate(muzzleFlash,spawnPosition.position,spawnPosition.rotation);
            newMuzzleFlash.transform.parent = spawnPosition;
            // newMuzzleFlash.transform.parent = GameObject.FindWithTag("Player").transform.Find("WeaponTip");

            timeBetweenShots = 0;
        }
    }

    private Vector3 GetSpawnDirection()
    {
        Vector3 spawnDirection = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(transform.position, new Vector3(Screen.width / 2, Screen.height / 2, 0), Color.magenta,5);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray,out raycastHit))
            spawnDirection = (raycastHit.point - spawnPosition.position).normalized;
        else
            spawnDirection = spawnPosition.forward;

        return spawnDirection;
    }

    public void SetWeapon(WeaponItem weaponItem)
    {
        this.weaponItem = weaponItem;
    }
    public GameObject GetWeapon()
    {
        return weaponPrefab;
    }
    public void SetFireRate(int fireRate)
    {
        this.fireRate = fireRate;
    }
    public int GetFireRate()
    {
        return fireRate;
    }
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    public int GetDamage()
    {
        return damage;
    }
}
