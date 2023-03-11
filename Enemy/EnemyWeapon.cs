using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    EnemyAim enemyAim;
    public override void Init()
    {
        fireShotSound = GetComponent<AudioSource>();
        SetWeaponStats();
        SetWeaponAim();

        //Enemy is on 10th layer and Projectile is on 11th layer
        //Ignoring collision between layers enemy and projectile
        Physics.IgnoreLayerCollision(10,12,true);
        // Debug.Log($"weapon range:{GetRange()}");
    }
    protected override void SetWeaponAim()
    {
        enemyAim = transform.parent.GetComponent<EnemyAim>();
    }
    public override void Fire()
    {
        timeBetweenShots += Time.deltaTime;
        Debug.Log($"entering fire");
        if(firing && timeBetweenShots >= 1f / fireRate && bulletsLeft > 0)
        {
            Debug.Log($"Enemy firing");
            Vector3 spawnDirection = enemyAim.transform.forward;
            // Instantiate the projectile and set its position and rotation
            EnemyProjectile projectileObj = Instantiate(projectilePrefab, spawnPosition.position, Quaternion.LookRotation(spawnDirection)).GetComponent<EnemyProjectile>();
            //Play fire shot sound effect
            fireShotSound.Play();
            // Set the damage of the projectile
            projectileObj.SetDamage(damage);
            // Instantiate the muzzle flash effect for the gun
            GameObject newMuzzleFlash = Instantiate(muzzleFlash, spawnPosition.position, Quaternion.LookRotation(spawnDirection));
            newMuzzleFlash.transform.parent = enemyAim.transform;
            // Reset the time between shots
            timeBetweenShots = 0;
            bulletsLeft--;
        }
        else if (bulletsLeft == 0)
            Reload();
    }
    public override void Reload()
    {
        bulletsLeft = magazineSize;
    }
}
