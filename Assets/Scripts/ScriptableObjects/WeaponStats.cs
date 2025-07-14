using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : ScriptableObject
{
    [SerializeField, Range(0f, 20f)] private float timeBetweenShots;  
    public float TimeBetweenShots => timeBetweenShots;

    [SerializeField] private int damage;
    public int Damage => damage;

    [SerializeField] private LayerMask targetMask;
    public LayerMask TargetMask => targetMask;
}
