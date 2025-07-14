using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ShipST_AttackChase : ShipST_Attack
{
    public ShipST_AttackChase(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        if (controller.AttackTarget == null) stateMachine.ChangeState(controller.IdleState);

        if (controller.AttackTarget != null && !controller.WeaponSensor.CanDetectTarget(controller.AttackTarget))
            stateMachine.ChangeState(controller.ChaseState);

        movement.CalculateDesiredDirection(false);
        movement.RotateTowardsDirection(movement.DesiredDirection);
        if (controller.AttackTarget != null && !controller.WeaponSensor.CheckDistanceWithMultiplier(controller.AttackTarget, 0.4f))
            movement.MoveShip(true, true);

        controller.FireWeapons();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
