using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISteering
{
    public Vector3 MoveDirection();

    public void SetTarget(Transform target);
}
