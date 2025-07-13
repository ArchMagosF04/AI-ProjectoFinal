using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Patrol : BaseState
{
    private FighterController controllerFT;
    private PathfindPatrol pathfindPatrol;

    public Fighter_Patrol(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
        controllerFT = (FighterController)controller;
        pathfindPatrol = controller.PathfindPatrol;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        movement.ChangeSteering(ShipMovement.SteeringMode.Seek);

        pathfindPatrol.StartPatrolFromLocation(controller.transform);

        movement.ChangeTarget(pathfindPatrol.TargetLocation);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(pathfindPatrol.ProgressPatrol()) movement.ChangeTarget(pathfindPatrol.TargetLocation);

        movement.CalculateDesiredDirection();
        movement.RotateTowardsDirection(movement.DesiredDirection);
        movement.MoveShip();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (controller.AttackTarget == null)
        {
            ShipID targetId = null;

            controllerFT.ShipRadar.RadarBurst();

            targetId = controllerFT.ShipRadar.GetFavoriteTarget();

            if (targetId != null)
            {
                movement.ChangeTarget(targetId.Transform);
                controller.SelectAttackTarget(targetId.Transform);

                stateMachine.ChangeState(controllerFT.FindTargetState);
            }
        }
    }
}
