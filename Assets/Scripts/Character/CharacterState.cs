
using UnityEngine;

public abstract class CharacterState : IState
{
    protected FiniteStateMachine stateMachine;


    protected Character character;
    private int hashAnimation;

    protected Collider[] detectedTarget;

    private float checkRate = 0.05f;
    private float lastCheckTime;

    protected bool isDetected = false;
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
        if(Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            isDetected = DetectedTarget();
        }
    }

    public virtual void PhysicsUpdate()
    {
    }

    protected void Move()
    {
        if(detectedTarget[0] != null)
        {
            Vector3 moveDirection = GetMoveDirection(detectedTarget[0].transform);
            Rotate(moveDirection);
            Move(moveDirection);
        }
    }

    protected void Move(Vector3 direction)
    {
        character.controller.Move(direction * character.characterData.moveSpeed);
    }

    private Vector3 GetMoveDirection(Transform target)
    {
        if (target == null)
            return Vector3.zero;

        Vector3 dir = target.transform.position - character.transform.position;
        dir.y = 0;
        dir.Normalize();


        return dir;
    }

    protected void Rotate(Vector3 direction)
    {
        if (direction != null)
        {
            Transform characterTransform = character.transform;
            Quaternion targetRot = Quaternion.LookRotation(direction);
            characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, targetRot, character.characterData.rotSpeed * Time.deltaTime);
        }
    }

    protected bool DetectedTarget()
    {
        detectedTarget = Physics.OverlapSphere(character.transform.position, character.characterData.patrolRadius, character.characterData.target);

        if (detectedTarget.Length > 0)
            return true;
        else
            return false;
    }
}
