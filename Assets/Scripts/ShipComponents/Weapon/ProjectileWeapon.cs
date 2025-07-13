using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [Header("Projectile")]
    [SerializeField] private Bullet projectilePrefab;

    public override void FireWeapon()
    {
        Vector3 direction = (target.position.NoY() - transform.position.NoY()).normalized;

        Bullet bullet = Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(direction, Vector3.up));
        bullet.FireBullet();
    }
}
