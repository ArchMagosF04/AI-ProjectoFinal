using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipST_FindTarget : BaseState
{
    private PathfindSeek pathfind;

    public ShipST_FindTarget(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
        this.pathfind = controller.PathfindSeek;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        if (movement.TargetLocation == null) return;

        pathfind.GetRouteToPoint(movement.TargetLocation);
        movement.ChangeSteering(ShipMovement.SteeringMode.Seek);
        ChangeTargetDest();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (controller.ShipSensor.CanDetectTarget(controller.AttackTarget))
        {
            stateMachine.ChangeState(controller.ChaseState);
        }

        if (pathfind.ProgressChase()) ChangeTargetDest();
        if (pathfind.ReachedFinalDestination)
        {
            movement.ChangeTarget(null);
            stateMachine.ChangeState(controller.IdleState);
        }


        movement.CalculateDesiredDirection();
        movement.RotateTowardsDirection(movement.DesiredDirection);
        movement.MoveShip();
    }

    private void ChangeTargetDest() 
    {
       if (pathfind.TargetLocation != null) movement.ChangeTarget(pathfind.TargetLocation);
    }
}
