using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvetteController : ShipController
{
    #region Variables

    //States

    #endregion

    #region Unity Methods

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    #endregion

    protected override void SetShipID()
    {
        ShipID = new ShipID(ShipTypes.Corvette, movement, transform);
    }

    protected override void SetUpStateMachine()
    {
        base.SetUpStateMachine();

        IdleState = new ShipST_Idle(this, stateMachine, movement);
        FleeState = new ShipST_Flee(this, stateMachine, movement);
        FindTargetState = new ShipST_FindTarget(this, stateMachine, movement);
        PatrolState = new ShipST_Patrol(this, stateMachine, movement);
        ChaseState = new ShipST_Chase(this, stateMachine, movement);
        AttackState = new ShipST_Attack(this, stateMachine, movement);

        stateMachine.Initialize(IdleState);
    }
}
