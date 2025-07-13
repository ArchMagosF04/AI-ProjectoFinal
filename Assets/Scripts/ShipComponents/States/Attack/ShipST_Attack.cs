using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipST_Attack : BaseState
{
    public ShipST_Attack(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        movement.ChangeSteering(ShipMovement.SteeringMode.Seek);
        movement.ChangeTarget(controller.AttackTarget);
        controller.ToggleWeaponManualControl(true);
        controller.AimWeaponsAtTarget();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (controller.AttackTarget == null) stateMachine.ChangeState(controller.IdleState);

        if (controller.AttackTarget != null && !controller.WeaponSensor.CanDetectTarget(controller.AttackTarget)) stateMachine.ChangeState(controller.ChaseState);

        movement.CalculateDesiredDirection();
        movement.RotateTowardsDirection(movement.DesiredDirection);
        

        controller.FireWeapons();
    }

    public override void OnExit()
    {
        base.OnExit();
        controller.ToggleWeaponManualControl(false);
    }
}
