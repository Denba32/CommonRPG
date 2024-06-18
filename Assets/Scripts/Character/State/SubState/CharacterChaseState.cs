using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChaseState : GroundState
{
    public CharacterChaseState(Character character, FiniteStateMachine stateMachine, int hashAnimation) : base(character, stateMachine, hashAnimation)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Chase ป๓ลย");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (DetectedTarget())
            Move(detectedTarget[0].transform.localPosition);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
