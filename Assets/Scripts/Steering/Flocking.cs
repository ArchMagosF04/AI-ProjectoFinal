using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    [SerializeField, Range(1, 4)] private int boidPriority;

    [Header("Entity Detection")]

    [SerializeField] private float separationRadius;
    [SerializeField] private float cohesionRadius;
    [SerializeField] private float alligmentRadius;
    [SerializeField] private LayerMask teamLayer;

    [Header("Weights")]

    [SerializeField, Range(0, 3f)] private float separationWeight;
    [SerializeField, Range(0, 1f)] private float cohesionWeight;
    [SerializeField, Range(0, 1f)] private float aligmentWeight;

    [Header("Gizmos")]

    [SerializeField] private bool DebugGizmos = true;

    private Collider[] GetNeighbours(float radius)
    {
        return Physics.OverlapSphere(transform.position, radius, teamLayer);
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

        return separationForce * separationWeight;
    }

    //Add the Two

    public Vector3 CalculateAlligment()
    {
        Vector3 neighboursForward = Vector3.zero;
        Collider[] neighbours = GetNeighbours(alligmentRadius);

        if (neighbours.Length == 0) return Vector3.zero;

        foreach (Collider col in neighbours)
        {
            if (col.gameObject == gameObject) continue;
            neighboursForward += col.transform.forward;
        }

        if (neighboursForward != Vector3.zero)
        {
            neighboursForward.Normalize();
        }

        return neighboursForward * aligmentWeight;
    }

    public Vector3 CalculateCohesion()
    {
        Vector3 averagePosition = Vector3.zero;
        Collider[] neighbours = GetNeighbours(alligmentRadius);

        if (neighbours.Length == 0) return Vector3.zero;

        foreach (Collider col in neighbours)
        {
            if (col.gameObject == gameObject) continue;
            averagePosition += col.transform.position;
        }

        averagePosition /= neighbours.Length;
        Vector3 cohesionDir = (averagePosition - transform.position).normalized;
        return cohesionDir * cohesionWeight;
    }

    private void OnDrawGizmos()
    {
        if (!DebugGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, separationRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, cohesionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alligmentRadius);
    }
}
