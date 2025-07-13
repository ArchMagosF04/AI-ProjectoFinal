using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : ShipController
{
    #region Variables

    //States
    public Fighter_Idle IdleState { get; private set; }
    public Fighter_Flee FleeState {  get; private set; }
    public Fighter_FindTarget FindTargetState { get; private set; }
    public Fighter_Patrol PatrolState { get; private set; }
    public Fighter_Chase ChaseState { get; private set; }

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
        ShipHealth.OnLowHealth += FleeOnLowHealth;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ShipHealth.OnLowHealth -= FleeOnLowHealth;
    }

    #endregion

    protected override void SetShipID()
    {
        ShipID = new ShipID(ShipTypes.Fighter, movement, transform);
    }

    protected override void SetUpStateMachine()
    {
        base.SetUpStateMachine();

        IdleState = new Fighter_Idle(this, stateMachine, movement);
        FleeState = new Fighter_Flee(this, stateMachine, movement);
        FindTargetState = new Fighter_FindTarget(this, stateMachine, movement);
        PatrolState = new Fighter_Patrol(this, stateMachine, movement);
        ChaseState = new Fighter_Chase(this, stateMachine, movement);

        stateMachine.Initialize(IdleState);
    }

    private void FleeOnLowHealth()
    {
        stateMachine.ChangeState(FleeState);
        ShipHealth.OnLowHealth -= FleeOnLowHealth;
    }
}
