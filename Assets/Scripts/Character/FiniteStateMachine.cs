using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    public IState CurrentState { get; private set; }

    public void Initialize(IState initial)
    {
        CurrentState = initial;
    }
    public void ChangeState(IState nextState)
    {
        CurrentState?.Exit();
        CurrentState = nextState;
        CurrentState?.Enter();
    }

    private void Update() => CurrentState?.LogicUpdate();
    private void FixedUpdate() => CurrentState?.PhysicsUpdate();
}
