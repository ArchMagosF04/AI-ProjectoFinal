using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Chase : BaseState
{
    private FighterController controllerFT;

    public Fighter_Chase(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
        controllerFT = (FighterController)controller;
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

        if (!controller.ShipSensor.CanDetectTarget(controller.AttackTarget))
        {
            controllerFT.SelectAttackTarget(null);
            stateMachine.ChangeState(controllerFT.IdleState);
        }

        movement.CalculateDesiredDirection();
        movement.RotateTowardsDirection(movement.DesiredDirection);
        movement.MoveShip();
    }
}
