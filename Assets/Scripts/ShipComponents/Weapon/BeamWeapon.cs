using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeapon : Weapon
{
    [Header("BeamStats")]
    [SerializeField] private int damage;
    [SerializeField] protected float duration;

    private LineRenderer lineRenderer;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        lineRenderer.enabled = false;
    }

    public override void FireWeapon()
    {
        lineRenderer.SetPosition(0, transform.position);

        Vector3 direction = (target.position.NoY() - transform.position.NoY());

        if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<HealthController>(out HealthController health))
            {
                Debug.Log("Fire");
                health.TakeDamage(damage);
                StartCoroutine(DrawLine());
            }
        }
    }

    private IEnumerator DrawLine()
    {
        lineRenderer.SetPosition(1, target.position);
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(duration);

        lineRenderer.enabled = false;
    }
}
