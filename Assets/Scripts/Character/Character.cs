using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    #region Components

    [field: SerializeField]
    public Animator animator { get; private set; }

    [field: SerializeField]
    public NavMeshAgent agent { get; private set; }

    [field: SerializeField]
    public FiniteStateMachine StateMachine { get; private set; }

    [field: SerializeField]
    public CharacterController controller { get; private set; }

    #endregion
    [field : SerializeField]
    public CharacterAnimationData AnimationData { get; private set; }

    [field : SerializeField]
    public CharacterSO characterData { get; private set; }

    #region STATE 

    public CharacterIdleState IdleState { get; private set; }
    public CharacterChaseState ChaseState { get; private set; }



    #endregion


    private void Awake()
    {
        AnimationData.Init();

        StateMachine = new FiniteStateMachine();
        IdleState = new CharacterIdleState(this, StateMachine, AnimationData.IdleHash);
        ChaseState = new CharacterChaseState(this, StateMachine, AnimationData.WalkHash);

    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState?.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState?.PhysicsUpdate();
    }

}
