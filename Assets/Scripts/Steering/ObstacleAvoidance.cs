using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    [Header("Stats")]

    [SerializeField] private float detectionRange; //Range at which detects obstacles.
    [SerializeField] private float avoidForce; //Force of the movement.
    [SerializeField] private LayerMask obstacleMask; //Layer where to search for obstacles.

    [Header("Debug")]

    [SerializeField] private bool DebugGizmos;


    public Vector3 Avoid()
    {
        var colliders = Physics.OverlapSphere(transform.position, detectionRange, obstacleMask); //Searches for obstacles.

        if (colliders.Length == 0) return Vector3.zero;

        float minDist = detectionRange + 1;
        Collider closestCol = null;

        for (int i = 0; i < colliders.Length; i++) //Finds the closest one of them all.
        {
            float currentDist = Vector3.Distance(transform.position, colliders[i].ClosestPoint(transform.position));

            if (currentDist < minDist)
            {
                closestCol = colliders[i];
                minDist = currentDist;
            }
        }

        if (closestCol == null) return Vector3.zero;

        Vector3 avoidDir = (transform.position.NoY() - closestCol.ClosestPoint(transform.position.NoY())).normalized * avoidForce; //Sets a movement vector heading away from the obstacle.

        avoidDir *= Mathf.Lerp(1, 0, Vector3.Distance(transform.position.NoY(), closestCol.ClosestPoint(transform.position.NoY())) / detectionRange);

        return avoidDir;
    }

    private void OnDrawGizmos()
    {
        if (!DebugGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
