using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Idle : BaseState
{
    private FighterController controllerFT;

    private float minIdleTime = 1.5f;
    private float maxIdleTime = 5f;

    private float idleDuration;

    public Fighter_Idle(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
        controllerFT = (FighterController)controller;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        movement.ChangeSteering(ShipMovement.SteeringMode.None);
        movement.ChangeTarget(null);
        controllerFT.SelectAttackTarget(null);
        idleDuration = Random.Range(minIdleTime, maxIdleTime);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Time.time > startTime + idleDuration)
        {
            stateMachine.ChangeState(controllerFT.PatrolState);
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
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

    public override void OnExit()
    {
        base.OnExit();
    }
}
