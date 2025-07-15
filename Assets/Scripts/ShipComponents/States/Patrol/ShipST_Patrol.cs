using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipST_Patrol : BaseState
{
    private PathfindPatrol pathfindPatrol;

    public ShipST_Patrol(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
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

        movement.CalculateDesiredDirection(true);
        movement.RotateTowardsDirection(movement.DesiredDirection);
        movement.MoveShip(true, true);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (controller.AttackTarget == null)
        {
            ShipID targetId = null;

            controller.ShipRadar.RadarBurst();

            targetId = controller.ShipRadar.GetFavoriteTarget();

            if (targetId != null)
            {
                movement.ChangeTarget(targetId.Transform);
                controller.SelectAttackTarget(targetId.Transform);

                stateMachine.ChangeState(controller.FindTargetState);
                return;
            }
        }
    }
}
