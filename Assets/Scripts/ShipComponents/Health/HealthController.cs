using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject[] particles;
    public int CurrentHealth { get; private set; }

    public Action OnDeath;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            ShipDestroyed();
        }
    }

    private void ShipDestroyed()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            Instantiate(particles[i], transform.position, Quaternion.identity);
        }
        
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
