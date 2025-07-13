using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer_Seek : ISteering
{
    private Transform transform;
    private Transform target;

    public Steer_Seek(Transform transform, Transform target)
    {
        this.transform = transform;
        this.target = target;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public Vector3 MoveDirection()
    {
        if (target == null) return Vector3.zero;

        Vector3 desiredDirection = (target.position.NoY() - transform.position.NoY()).normalized;

        return desiredDirection;
    }
}
