using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipST_Idle : BaseState
{
    private float minIdleTime = 1.5f;
    private float maxIdleTime = 5f;

    private float idleDuration;

    public ShipST_Idle(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        movement.ChangeSteering(ShipMovement.SteeringMode.None);
        movement.ChangeTarget(null);
        controller.SelectAttackTarget(null);
        idleDuration = Random.Range(minIdleTime, maxIdleTime);

        idleDuration = minIdleTime; //Delete Later.
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Time.time > startTime + idleDuration)
        {
            stateMachine.ChangeState(controller.PatrolState);
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

            controller.ShipRadar.RadarBurst();

            targetId = controller.ShipRadar.GetFavoriteTarget();

            if (targetId != null)
            {
                movement.ChangeTarget(targetId.Transform);
                controller.SelectAttackTarget(targetId.Transform);

                stateMachine.ChangeState(controller.FindTargetState);
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
