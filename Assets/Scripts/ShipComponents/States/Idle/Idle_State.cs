using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_State : BaseState
{
    public Idle_State(ShipController controller, StateMachine stateMachine, ShipMovement movement) : base(controller, stateMachine, movement)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }
}
