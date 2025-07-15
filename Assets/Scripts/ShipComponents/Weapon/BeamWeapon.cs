using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BeamWeapon : Weapon
{
    private BeamWeaponStats beamStats;

    private LineRenderer lineRenderer;

    protected override void Awake()
    {
        base.Awake();
        beamStats = (BeamWeaponStats)stats;
        lineRenderer = GetComponent<LineRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        lineRenderer.enabled = false;
    }

    public override void FireWeapon()
    {
        if (Target == null || Time.time < timeOfLastShot + beamStats.TimeBetweenShots) return;

        if (!sensor.CanDetectTarget(Target)) return;

        lineRenderer.SetPosition(0, transform.position);

        Vector3 direction = (Target.position - transform.position);

        if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<HealthController>(out HealthController health))
            {
                if (beamStats.ImpactParticle != null) Instantiate(beamStats.ImpactParticle, hit.point, Quaternion.identity);
                timeOfLastShot = Time.time;
                if (sounds != null) SoundManager.Instance.CreateSound().WithSoundData(sounds.soundData[0]).WithPosition(transform.position).WithRandomPitch().Play();
                StartCoroutine(DrawLine());
                health.TakeDamage(beamStats.Damage);
            }
        }
    }

    private IEnumerator DrawLine()
    {
        if (Target == null) StopAllCoroutines();
        lineRenderer.SetPosition(1, Target.position);
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(beamStats.LaserDuration);

        lineRenderer.enabled = false;
    }
}
