using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StateMachine
{
    public Action OnStateChange;

    public BaseState CurrentState { get; private set; }

    public void Initialize(BaseState startingState)
    {
        CurrentState = startingState;
        CurrentState.OnEnter();
        OnStateChange?.Invoke();
    }

    public void ChangeState(BaseState newState)
    {
        CurrentState.OnExit();
        CurrentState = newState;
        OnStateChange?.Invoke();
        CurrentState.OnEnter();
    }
}
