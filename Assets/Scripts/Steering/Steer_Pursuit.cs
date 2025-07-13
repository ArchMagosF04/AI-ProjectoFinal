using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer_Pursuit : ISteering
{
    private Transform transform;
    private ShipMovement target;
    private float timePrediction;
    private float closeEnough;
    public Vector3 FuturePos { get; private set; }

    public Steer_Pursuit(Transform transform, Transform target, float timePrediction, float closeEnough)
    {
        this.transform = transform;
        if (target != null) this.target = target.GetComponent<ShipMovement>();
        this.timePrediction = timePrediction;
        this.closeEnough = closeEnough;
    }

    public void SetTarget(Transform target)
    {
        if (target != null) this.target = target.GetComponent<ShipMovement>();
    }

    public Vector3 MoveDirection()
    {
        if (target ==  null) return Vector3.zero;

        float distanceFromTarget = Vector3.Distance(transform.position.NoY(), target.transform.position.NoY());
        distanceFromTarget = distanceFromTarget.Remap(0, distanceFromTarget, 0, 20f);

        if (distanceFromTarget < closeEnough) { distanceFromTarget = 0; }

        Vector3 futurePosition = target.transform.position.NoY() + (target.MoveVector) * timePrediction * distanceFromTarget;
        FuturePos = futurePosition;

        Vector3 desiredDirection = (futurePosition.NoY() - transform.position.NoY()).normalized;

        return desiredDirection;
    }
}
