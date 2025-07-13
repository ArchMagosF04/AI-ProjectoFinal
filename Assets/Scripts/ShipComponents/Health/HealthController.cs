using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth;
    [field: SerializeField] public int CurrentHealth { get; private set; }
    [SerializeField, Range(1f, 100f)] private int lowHealthThreshold;

    [Header("Particles")]
    [SerializeField] private GameObject[] deathParticles;
    
    //Events
    public Action OnDeath;
    public Action OnLowHealth;

    //Other
    public bool LowHealthInvoked { get; private set; }

    private void Start()
    {
        CurrentHealth = maxHealth;
        LowHealthInvoked = false;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (!LowHealthInvoked && CurrentHealth <= maxHealth * (lowHealthThreshold/100))
        {
            OnLowHealth?.Invoke();
            LowHealthInvoked = true;
        }

        if (CurrentHealth <= 0)
        {
            ShipDestroyed();
        }
    }

    private void ShipDestroyed()
    {
        for (int i = 0; i < deathParticles.Length; i++)
        {
            Instantiate(deathParticles[i], transform.position, Quaternion.identity);
        }
        
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
