using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //Components
    public ShipMovement Movement { get; private set; }

    protected virtual void Awake()
    {
        Movement = GetComponent<ShipMovement>();
    }

    protected virtual void Update()
    {
        Movement.CalculateDesiredDirection();
        Movement.RotateTowardsDirection(Movement.DesiredDirection);
        Movement.MoveShip();
    }

    protected virtual void FixedUpdate()
    {

    }
}
