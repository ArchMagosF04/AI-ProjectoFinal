using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer_Flee : ISteering
{
    private Transform transform;
    private Transform target;

    public Steer_Flee(Transform transform, Transform target)
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

        Vector3 desiredDirection = (transform.position.NoY() - target.position.NoY()).normalized;

        return desiredDirection;
    }
}
