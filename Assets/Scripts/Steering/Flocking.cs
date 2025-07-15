using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    [field: SerializeField, Range(1, 4)] public int BoidPriority;

    [Header("Entity Detection")]

    [SerializeField] private float separationRadius;
    [SerializeField] private float cohesionRadius;
    [SerializeField] private float alignmentRadius;
    [SerializeField] private LayerMask teamLayer;

    [Header("Weights")]

    [SerializeField, Range(0, 3f)] private float separationWeight;
    [SerializeField, Range(0, 1f)] private float cohesionWeight;
    [SerializeField, Range(0, 1f)] private float alignmentWeight;

    [Header("Gizmos")]

    [SerializeField] private bool DebugGizmos = true;

    public bool UseFlocking { get; private set; }
    private ShipMovement movement;

    private Vector3 separationDir;
    private Vector3 alignmentDir;
    private Vector3 cohesionDir;

    private void Awake()
    {
        movement = GetComponent<ShipMovement>();
    }

    private void Start()
    {
        UseFlocking = true;
    }

    public void ToggleFlocking(bool value) => UseFlocking = value;

    private Collider[] GetNeighbours(float radius)
    {
        return Physics.OverlapSphere(transform.position, radius, teamLayer);
    }

    private ShipMovement[] GetFlockingNeighbours(float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, teamLayer);

        List<ShipMovement> boids = new List<ShipMovement>();

        foreach (Collider col in colliders)
        {
            if(col.TryGetComponent<ShipMovement>(out ShipMovement flock))
            {
                boids.Add(flock);
            }
        }

        return boids.ToArray();
    }

    public Vector3 CalculateSeparation()
    {
        Vector3 separationForce = Vector3.zero;
        Collider[] neighbours = GetNeighbours(separationRadius);

        if (neighbours.Length == 0) return Vector3.zero;

        foreach (Collider col in neighbours)
        {
            if (col.gameObject == gameObject) continue;

            Vector3 dir = (col.transform.position.NoY() - transform.position.NoY());
            float distance = dir.magnitude;
            Vector3 away = -dir.normalized;

            if (distance > 0)
            {
                separationForce += away / distance;
            }
        }

        separationDir = separationForce.normalized;
        return separationForce * separationWeight;
    }

    public Vector3 CalculateAlignment()
    {
        Vector3 neighboursMoveDir = Vector3.zero;
        ShipMovement[] neighbours = GetFlockingNeighbours(alignmentRadius);

        if (neighbours.Length == 0) return Vector3.zero;

        foreach (ShipMovement ship in neighbours)
        {
            if (!ship.Boid.UseFlocking || ship.gameObject == gameObject || ship.Boid.BoidPriority > BoidPriority) continue;
            neighboursMoveDir += Vector3.Lerp(movement.DesiredDirection, ship.DesiredDirection, alignmentWeight);
        }

        if (neighboursMoveDir != Vector3.zero)
        {
            neighboursMoveDir /= neighbours.Length;
            neighboursMoveDir.Normalize();
        }
        alignmentDir = neighboursMoveDir;
        return neighboursMoveDir;
    }

    public Vector3 CalculateCohesion()
    {
        Vector3 averagePosition = Vector3.zero;
        ShipMovement[] neighbours = GetFlockingNeighbours(cohesionRadius);

        if (neighbours.Length == 0) return Vector3.zero;

        foreach (ShipMovement ship in neighbours)
        {
            if (!ship.Boid.UseFlocking || ship.Boid.BoidPriority > BoidPriority) continue;
            averagePosition += ship.transform.position/*Vector3.Lerp(transform.position, ship.transform.position, cohesionWeight)*/;
        }

        if (averagePosition == Vector3.zero)
        {
            Debug.Log("GotNothing");
            return Vector3.zero;
        }

        averagePosition /= neighbours.Length;
        cohesionDir = (averagePosition - transform.position).normalized;
        return cohesionDir * cohesionWeight;
    }

    private void OnDrawGizmos()
    {
        if (!DebugGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, separationRadius);
        Gizmos.DrawRay(transform.position, separationDir * separationRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, cohesionRadius);
        Gizmos.DrawRay(transform.position, cohesionDir * cohesionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alignmentRadius);
        Gizmos.DrawRay(transform.position, alignmentDir * alignmentRadius);
    }
}
