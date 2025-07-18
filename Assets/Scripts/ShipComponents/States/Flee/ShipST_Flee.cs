using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipST_Flee : BaseState
{
    private float maxFleeTime = 10f;
    private float maxFleeDistance = 850f;

    private Vector3 startLocation;

    public ShipST_Flee(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        movement.ChangeSteering(ShipMovement.SteeringMode.Flee);
        movement.ChangeTarget(null);
        movement.Boid.ToggleFlocking(false);

        startLocation = controller.transform.position.NoY();

        GetTargetToFleeFrom();
    }

    private void GetTargetToFleeFrom()
    {
        ShipID targetId = null;

        controller.ShipRadar.RadarBurst();

        targetId = controller.ShipRadar.GetClosestTarget();

        if (targetId != null) movement.ChangeTarget(targetId.Transform);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Time.time > startTime + maxFleeTime || Vector3.Distance(startLocation, controller.transform.position.NoY()) >= maxFleeDistance)
        {
            stateMachine.ChangeState(controller.IdleState);
            return;
        }

        movement.CalculateDesiredDirection(false);
        movement.RotateTowardsDirection(movement.DesiredDirection);
        movement.MoveShip(true, false);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (movement.TargetLocation == null) GetTargetToFleeFrom();
    }

    public override void OnExit()
    {
        base.OnExit();
        movement.Boid.ToggleFlocking(true);
    }
}
