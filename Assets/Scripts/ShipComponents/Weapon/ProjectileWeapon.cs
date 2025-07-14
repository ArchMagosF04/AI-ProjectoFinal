using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    protected ProjectileWeaponStats projectileStats;

    protected override void Awake()
    {
        base.Awake();
        projectileStats = (ProjectileWeaponStats)stats;
    }

    public override void FireWeapon()
    {
        if (Target == null || Time.time < timeOfLastShot + projectileStats.TimeBetweenShots) return;

        if (!sensor.CanDetectTarget(Target)) return;

        Vector3 direction = (Target.position - transform.position).normalized;

        Bullet bullet = Instantiate(projectileStats.BulletPrefab, transform.position, Quaternion.LookRotation(direction, Vector3.up));
        bullet.SetStats(projectileStats.Speed, projectileStats.Damage, projectileStats.TargetMask, projectileStats.LifeTime);
        bullet.FireBullet();
        timeOfLastShot = Time.time;
    }
}
