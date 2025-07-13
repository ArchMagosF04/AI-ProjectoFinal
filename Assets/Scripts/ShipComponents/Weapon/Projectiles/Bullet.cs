using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float lifeTime;

    private Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void FireBullet()
    {
        rb.velocity = transform.forward * speed; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (/*other.gameObject.layer == targetMask*/  other.CompareTag("RedTeam"))
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
