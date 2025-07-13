using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_FindTarget : BaseState
{
    private FighterController controllerFT;
    private PathfindSeek pathfind;

    public Fighter_FindTarget(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
        controllerFT = (FighterController)controller;
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
            stateMachine.ChangeState(controllerFT.ChaseState);
        }

        if (pathfind.ProgressChase()) ChangeTargetDest();
        if (pathfind.ReachedFinalDestination)
        {
            movement.ChangeTarget(null);
            stateMachine.ChangeState(controllerFT.IdleState);
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
