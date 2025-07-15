using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/Weapon/BeamWeapon", order = 1)]
public class BeamWeaponStats : WeaponStats
{
    [SerializeField] private float laserDuration;
    public float LaserDuration => laserDuration;
}
