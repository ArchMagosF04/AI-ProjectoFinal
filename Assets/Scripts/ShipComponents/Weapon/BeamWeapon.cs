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
        if (Time.time < timeOfLastShot + timeBetweenShots || Target == null) return;

        lineRenderer.SetPosition(0, transform.position);

        Vector3 direction = (Target.position.NoY() - transform.position.NoY());

        if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<HealthController>(out HealthController health))
            {
                Debug.Log("Fire");
                health.TakeDamage(damage);
                timeOfLastShot = Time.time;
                StartCoroutine(DrawLine());
            }
        }
    }

    private IEnumerator DrawLine()
    {
        lineRenderer.SetPosition(1, Target.position);
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(duration);

        lineRenderer.enabled = false;
    }
}
