using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] WeaponItem weaponItem;
    [SerializeField] Transform spawnPosition;
    AudioSource fireShotSound;
    WeaponAim weaponAim;
    int fireRate;
    int damage;
    int magazineSize;
    int bulletsLeft;
    float timeBetweenShots;
    public GameObject muzzleFlash;
    public bool firing;
    DisplayAmmoUI displayAmmoUI;
    
    public void Init()
    {
        SetWeaponStats();
        SetWeaponAim();
        fireShotSound = GetComponent<AudioSource>();
        displayAmmoUI = transform.parent.GetComponentInParent<DisplayAmmoUI>();
        displayAmmoUI.Init(this);
        // displayAmmoUI.UpdateAmmoUI();
        // Debug.Log($"dmg:{damage}, fire rate:{fireRate}");
    }

    public void SetWeaponAim()
    {
        weaponAim = transform.parent.GetComponent<WeaponAim>();
    }

    private void SetWeaponStats()
    {
        // Debug.Log($"Setting weapon {weaponItem.itemName} with dmg {weaponItem.GetDamage()} and fire rate {weaponItem.GetFireRate()} with magazine size {weaponItem.GetMagazineSize()}");
        damage = weaponItem.GetDamage();
        fireRate = weaponItem.GetFireRate();
        magazineSize = weaponItem.GetMagazineSize();
        bulletsLeft = magazineSize;
    }

    private void FixedUpdate()
    {
        timeBetweenShots += Time.deltaTime;

        if(firing && timeBetweenShots >= 1f / fireRate && bulletsLeft > 0)
        {
            Fire();
        }
    }
    public void Fire()
    {
        if(firing && timeBetweenShots >= 1f / fireRate && bulletsLeft > 0)
        {
            Vector3 spawnDirection = weaponAim.transform.forward;
            // Instantiate the projectile and set its position and rotation
            Projectile projectileObj = Instantiate(projectilePrefab, spawnPosition.position, Quaternion.LookRotation(spawnDirection)).GetComponent<Projectile>();
            //Play fire shot sound effect
            fireShotSound.Play();
            // Set the damage of the projectile
            projectileObj.SetDamage(damage);
            // Instantiate the muzzle flash effect for the gun
            GameObject newMuzzleFlash = Instantiate(muzzleFlash, spawnPosition.position, Quaternion.LookRotation(spawnDirection));
            newMuzzleFlash.transform.parent = weaponAim.transform;
            // Reset the time between shots
            timeBetweenShots = 0;
            bulletsLeft--;
            displayAmmoUI.UpdateAmmoUI(this);
        }
    }
    public void Reload()
    {
        bulletsLeft = magazineSize;
        displayAmmoUI.UpdateAmmoUI(this);
    }
    private Vector3 GetSpawnDirection()
    {
        Vector3 spawnDirection = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(transform.position, new Vector3(Screen.width / 2, Screen.height / 2, 0), Color.magenta,5);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray,out raycastHit))
        {
            spawnDirection = raycastHit.point - transform.position;
            spawnDirection.y = 0;
            spawnDirection.Normalize();
            // spawnDirection = (raycastHit.point - spawnPosition.position).normalized;
        }
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
    public int GetMagazineSize()
    {
        return magazineSize;
    }
    public int GetBulletsLeft()
    {
        return bulletsLeft;
    }
}
