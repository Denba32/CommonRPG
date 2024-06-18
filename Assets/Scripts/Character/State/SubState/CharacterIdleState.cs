using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : GroundState
{
    public CharacterIdleState(Character character, FiniteStateMachine stateMachine, int hashAnimation) : base(character, stateMachine, hashAnimation)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Idle»óÅÂ");
        Move(Vector3.zero);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isDetected)
        {
            stateMachine.ChangeState(character.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
