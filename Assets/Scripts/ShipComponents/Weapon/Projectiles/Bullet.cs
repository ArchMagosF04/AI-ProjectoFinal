using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     private float speed;
     private int damage;
     private LayerMask targetMask;
     private float lifeTime;

    private Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetStats(float speed, int damage, LayerMask targetMask, float lifetime)
    {
        this.speed = speed;
        this.damage = damage;
        this.targetMask = targetMask;
        this.lifeTime = lifetime;
    }

    public void FireBullet()
    {
        rb.velocity = transform.forward * speed; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((targetMask.value & (1 << other.gameObject.layer)) > 0)
        {
            if (other.TryGetComponent<HealthController>(out HealthController health))
            {
                health.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
