using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public IState CurrentState { get; private set; }

    public void Initialize(IState initial)
    {
        CurrentState?.Enter();
        CurrentState = initial;
    }
    public void ChangeState(IState nextState)
    {
        CurrentState?.Exit();
        CurrentState = nextState;
        CurrentState?.Enter();
    }
}
