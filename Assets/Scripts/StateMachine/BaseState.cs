using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    protected ShipController controller;
    protected ShipMovement movement;
    protected StateMachine stateMachine;

    protected float startTime;

    public BaseState(ShipController controller, StateMachine stateMachine, ShipMovement movement)
    {
        this.controller = controller;
        this.stateMachine = stateMachine;
        this.movement = movement;
    }

    public virtual void OnEnter()
    {
        DoChecks();
        startTime = Time.time;
    }

    public virtual void OnUpdate() { }

    public virtual void OnFixedUpdate()
    {
        DoChecks();
    }

    public virtual void OnExit() { }

    public virtual void DoChecks() { }

    public virtual void SubscribeToEvents() { }
    public virtual void UnsubscribeToEvents() { }
}
