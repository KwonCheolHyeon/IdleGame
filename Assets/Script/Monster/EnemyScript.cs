using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public IMonsterState currentState { get; private set; }

    public IMonsterState idleState = new MonsterIdleScript();
    public IMonsterState runState = new MonsterRunScript();
    public IMonsterState attackState = new MonsterAttackScript();
    public IMonsterState deathState = new MonsterDeathScript();

    public Rigidbody2D rigid;
    public Animator animator; // Animator 추가
    public Transform targetPlayer; // 타겟팅된 적

    public float attackPoint;//공격력
    public float defensePoint; // 방어력
    public float monsterHealthPoint;
    public float runSpeed; // 걷는 속도
    public bool canAttack;//일반 공격
    public float attackTimer;//공격


    public float attackRange = 1.0f; // 공격을 시작할 거리
    private bool isAttackCooldownActive = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        monsterHealthPoint = 10.0f;
        runSpeed = 3.0f;
        attackTimer = 1.0f;
        canAttack = true;
        attackPoint = 1.0f;
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

    public void SetState(IMonsterState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }
        currentState = newState;
        currentState.EnterState(this);
    }

    public void TakeDamage(float attackPoint)
    {
        monsterHealthPoint -= attackPoint;
        if (monsterHealthPoint <= 0) 
        {
            SetState(deathState);
        }
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void AttackCoolTime(int type)
    {
        if (type == 0 && !isAttackCooldownActive)
        {
            StartCoroutine(CooldownCoroutine(attackTimer, () => canAttack = true, () => isAttackCooldownActive = false));
        }
    }

    private IEnumerator CooldownCoroutine(float cooldownTime, System.Action onCooldownEnd, System.Action onCooldownComplete)
    {
        onCooldownComplete();
        yield return new WaitForSeconds(cooldownTime);
        onCooldownEnd();
        onCooldownComplete();
    }

}
