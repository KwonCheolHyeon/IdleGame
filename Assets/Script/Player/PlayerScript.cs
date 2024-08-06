using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public ICharacterState currentState { get; private set; }

    public ICharacterState idleState = new IdleScript();
    public ICharacterState runState = new RunScript();
    public ICharacterState attackState = new AttackScript();
    public ICharacterState specialAttackState = new SpecialAttackScript();
    public ICharacterState deathState = new DeathScript();

    public Vector2 inputVec;
    public Rigidbody2D rigid;
    public Animator animator; // Animator 추가
    public Transform targetEnemy; // 타겟팅된 적

    public float runSpeed; // 걷는 속도
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        runSpeed = 3.0f;
        SetState(idleState);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SetState(ICharacterState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }
        currentState = newState;
        currentState.EnterState(this);
    }



}
