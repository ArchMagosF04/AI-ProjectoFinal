using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [Header("Projectile")]
    [SerializeField] private Bullet projectilePrefab;

    public override void FireWeapon()
    {
        if (Time.time < timeOfLastShot + timeBetweenShots || Target == null) return;

        Vector3 direction = (Target.position.NoY() - transform.position.NoY()).normalized;

        Bullet bullet = Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(direction, Vector3.up));
        bullet.FireBullet();
        timeOfLastShot = Time.time;
    }
}
