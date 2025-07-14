using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/Weapon/ProjectileWeapon", order = 0)]
public class ProjectileWeaponStats : WeaponStats
{
    [SerializeField] private float speed;
    public float Speed => speed;

    [SerializeField] private float lifeTime;
    public float LifeTime => lifeTime;

    [SerializeField] private Bullet bulletPrefab;
    public Bullet BulletPrefab => bulletPrefab;
}
