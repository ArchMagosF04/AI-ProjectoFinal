using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Flee : BaseState
{
    private FighterController controllerFT;

    private float maxFleeTime = 10f;
    private float maxFleeDistance = 700f;

    private Vector3 startLocation;

    public Fighter_Flee(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
        controllerFT = (FighterController)controller;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        movement.ChangeSteering(ShipMovement.SteeringMode.Flee);

        startLocation = controller.transform.position.NoY();

        GetTargetToFleeFrom();
    }

    private void GetTargetToFleeFrom()
    {
        ShipID targetId = null;

        controllerFT.ShipRadar.RadarBurst();

        targetId = controllerFT.ShipRadar.GetClosestTarget();

        if (targetId != null) movement.ChangeTarget(targetId.Transform);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Time.time > startTime + maxFleeTime || Vector3.Distance(startLocation, controller.transform.position.NoY()) >= maxFleeDistance)
        {
            stateMachine.ChangeState(controllerFT.IdleState);
        }

        movement.CalculateDesiredDirection();
        movement.RotateTowardsDirection(movement.DesiredDirection);
        movement.MoveShip();
    }
}
