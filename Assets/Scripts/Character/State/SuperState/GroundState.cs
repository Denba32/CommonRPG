using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : CharacterState
{
    public GroundState(Character character, FiniteStateMachine stateMachine, int hashAnimation) : base(character, stateMachine, hashAnimation)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    // GroundCheck
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!character.controller.isGrounded)
        {
            // TODO Ground -> Air
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
