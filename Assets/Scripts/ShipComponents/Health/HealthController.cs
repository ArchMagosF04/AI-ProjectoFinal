using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth;
    public int MaxHealth => maxHealth;
    [field: SerializeField] public int CurrentHealth { get; private set; }
    [SerializeField, Range(1f, 100f)] private float lowHealthThreshold;

    [SerializeField] private int healthRegen;
    [SerializeField] private float healthRegenTick;

    [Header("Particles")]
    [SerializeField] private GameObject[] deathParticles;

    [Header("Sound")]
    [SerializeField] private SoundLibraryObject sounds;
    
    //Events
    public Action OnDeath;
    public Action OnLowHealth;

    //Other
    public bool HealthRegenActive { get; private set; }
    public bool LowHealthInvoked { get; private set; }
    private float lastRegenTick;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        LowHealthInvoked = false;
        HealthRegenActive = false;
        lastRegenTick = Time.time;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(9999999);
        }

        HealthRegen();
    }

    public void ToggleHealthRegen(bool healthRegen) => HealthRegenActive = healthRegen;

    private void HealthRegen()
    { 
        if (HealthRegenActive && Time.time > lastRegenTick + healthRegenTick)
        {
            HealShip(healthRegen);
            lastRegenTick = Time.time;
        }
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

    public void HealShip(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > maxHealth) { CurrentHealth = maxHealth; }
    }

    public void HealShip(float percentage)
    {
        float healAmount = maxHealth * percentage;
        CurrentHealth += Mathf.RoundToInt(healAmount);
        if (CurrentHealth > maxHealth) { CurrentHealth = maxHealth; }
    }

    private void ShipDestroyed()
    {
        for (int i = 0; i < deathParticles.Length; i++)
        {
            Instantiate(deathParticles[i], transform.position, Quaternion.identity);
        }

        if (sounds != null) SoundManager.Instance.CreateSound().WithSoundData(sounds.soundData[0]).WithPosition(transform.position).WithRandomPitch().Play();

        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
