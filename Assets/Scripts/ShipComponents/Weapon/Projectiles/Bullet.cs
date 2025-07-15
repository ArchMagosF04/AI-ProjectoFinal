using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private float speed;
    private int damage;
    private LayerMask targetMask;
    private float lifeTime;

    private Rigidbody rb;

    private float startTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform.SetParent(GameManager.Instance.transform, true);
    }

    private void OnEnable()
    {
        startTime = Time.time;
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

    private void Update()
    {
        if (Time.time >= startTime + lifeTime) OnBulletDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((targetMask.value & (1 << other.gameObject.layer)) > 0)
        {
            if (other.TryGetComponent<HealthController>(out HealthController health))
            {
                health.TakeDamage(damage);
                OnBulletDeath();
            }
        }
    }

    private void OnBulletDeath()
    {
        rb.velocity = Vector3.zero;

        Destroy(this.gameObject);
    }
}
