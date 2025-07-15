using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipST_Chase : BaseState
{
    public ShipST_Chase(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        movement.ChangeSteering(ShipMovement.SteeringMode.Pursuit);
        movement.ChangeTarget(controller.AttackTarget);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (controller.AttackTarget != null && controller.WeaponSensor.CanDetectTargetWithDistanceMultiplier(controller.AttackTarget, 0.8f))
        {
            stateMachine.ChangeState(controller.AttackState);
            return;
        }
        if (controller.AttackTarget == null || !controller.ShipSensor.CanDetectTarget(controller.AttackTarget))
        {
            controller.SelectAttackTarget(null);
            stateMachine.ChangeState(controller.IdleState);
            return;
        }

        movement.CalculateDesiredDirection(true);
        movement.RotateTowardsDirection(movement.DesiredDirection);
        movement.MoveShip(true, true);
    }
}
