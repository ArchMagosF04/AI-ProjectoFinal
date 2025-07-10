using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    private Rigidbody rb;
    private LayerMask targetMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FireBullet()
    {
        rb.velocity = transform.forward * speed; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == targetMask)
        {
            if (other.TryGetComponent<HealthController>(out HealthController health))
            {
                Debug.Log("TargetHit");
                health.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
