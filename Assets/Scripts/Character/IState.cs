using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Exit();
    void DoCheck();
    void LogicUpdate();
    void PhysicsUpdate();
}
