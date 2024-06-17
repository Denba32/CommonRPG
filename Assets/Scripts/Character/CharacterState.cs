using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState : IState
{
    protected FiniteStateMachine stateMachine;
    protected Character character;
    private int hashAnimation;
    public CharacterState(Character character, FiniteStateMachine stateMachine, int hashAnimation)
    {
        this.stateMachine = stateMachine;
        this.character = character;
        this.hashAnimation = hashAnimation;

    }
    public virtual void DoCheck()
    {

    }

    public virtual void Enter()
    {
        DoCheck();
        character.animator.SetBool(hashAnimation, true);
    }

    public virtual void Exit()
    {
        character.animator.SetBool(hashAnimation, false);
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }
}
