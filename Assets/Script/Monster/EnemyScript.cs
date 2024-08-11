using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyScript : MonoBehaviour
{
    public IMonsterState currentState { get; private set; }

    public IMonsterState idleState = new MonsterIdleScript();
    public IMonsterState runState = new MonsterRunScript();
    public IMonsterState attackState = new MonsterAttackScript();
    public IMonsterState deathState = new MonsterDeathScript();
    public IMonsterState bossDeathState = new BossMonsterDeath();

    public Rigidbody2D rigid;
    public Animator animator; // Animator 추가
    public Transform targetPlayer; // 타겟팅된 적

    public float attackPoint;//공격력
    public float defensePoint; // 방어력
    public int monsterHealthPoint;
    public float baseMonsterAp;
    public float baseMonsterDp;
    public int baseMonsterHp;
    public float runSpeed; // 걷는 속도
    public bool canAttack;//일반 공격
    public float attackTimer;//공격


    public float attackRange = 1.0f; // 공격을 시작할 거리
    private bool isAttackCooldownActive = false;
    private bool isBoss = false;

    private void OnEnable()
    {
        targetPlayer = null;
        SetState(idleState);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        
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
        monsterHealthPoint -= (int)(attackPoint - defensePoint);
        if (monsterHealthPoint <= 0 ) 
        {
            if (isBoss)
            {
                SetState(bossDeathState);
            }
            else 
            {
                SetState(deathState);
            }
        }
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

    public void MonsterSetting(int stageCount)
    {
        baseMonsterAp = 5 * SetStageMonsterStat(stageCount);
        baseMonsterDp = 5 * SetStageMonsterStat(stageCount);
        baseMonsterHp = 10 * SetStageMonsterStat(stageCount);


        attackPoint = baseMonsterAp * stageCount;//공격력
        defensePoint = baseMonsterDp * stageCount; // 방어력
        monsterHealthPoint = baseMonsterHp * stageCount;

        runSpeed = 3.0f; // 걷는 속도
        canAttack = true;//일반 공격
        attackTimer = 1.5f;//공격
        isBoss = false;
    }

    public void BossSetting(int stageCount) 
    {
        baseMonsterAp = 5 * SetStageMonsterStat(stageCount);
        baseMonsterDp = 5 * SetStageMonsterStat(stageCount);
        baseMonsterHp = 10 * SetStageMonsterStat(stageCount);   


        attackPoint = 2 * baseMonsterAp * stageCount;//공격력
        defensePoint = 2 * baseMonsterDp * stageCount; // 방어력
        monsterHealthPoint = 2 *baseMonsterHp * stageCount;

        runSpeed = 3.0f; // 걷는 속도
        canAttack = true;//일반 공격
        attackTimer = 1.0f;//공격
        isBoss = true;
    }

    private int SetStageMonsterStat(int statgeCount) 
    {
        int stat = (statgeCount + 4) / 5;
        return stat;
    }

    public void DestroyBoss()
    {
        GameManager.Instance.MonsterStageUp();
        MonsterManager.Instance.bossSpawnOn = false;
        Destroy(transform.parent.gameObject);
    }

}
