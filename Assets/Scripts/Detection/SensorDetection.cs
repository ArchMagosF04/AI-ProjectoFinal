using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SensorDetection : MonoBehaviour
{
    [Header("Detection")]

    [SerializeField] private float detectionRange; //Range of vision.
    [SerializeField, Range(10f, 360f)] private float detectionAngle; //Angle of vision.
    [SerializeField] private LayerMask obstaclesMask; //Layer that blocks view.

    [Header("Debug")]

    [SerializeField] private bool debugGizmos = false; //Activate in inspector to visualize the Gizmos.

    public bool CanDetectTarget(Transform target) //Collective check of all conditions.
    {
        return (CheckDistance(target) && CheckAngle(target) && CheckView(target));
    }

    public bool CheckDistance(Transform target) //Checks if the target is within range of vision.
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return distance <= detectionRange;
    }

    public bool CheckDistanceWithMultiplier(Transform target, float multiplier) //Checks if the target is within the range of vision, modified by a multiplier.
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return distance <= detectionRange * multiplier;
    }

    public bool CheckAngle(Transform target) //Checks if the target is within the angle of vision.
    {
        Vector3 dir = target.position - transform.position;
        float angle = Vector3.Angle(transform.forward, dir);
        return angle <= detectionAngle / 2;
    }

    public bool CheckView(Transform target) //Checks if no obstacles obstructs the target out of view.
    {
        Vector3 dir = target.position - transform.position;

        return !Physics.Raycast(transform.position, dir.normalized, dir.magnitude, obstaclesMask);
    }

    private void OnDrawGizmos()
    {
        if (debugGizmos)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, detectionRange);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, detectionAngle / 2, 0) * transform.forward * detectionRange);
            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -detectionAngle / 2, 0) * transform.forward * detectionRange);
        }
    }
}
