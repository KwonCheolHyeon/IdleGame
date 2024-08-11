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
    public Animator animator; // Animator �߰�
    public Transform targetPlayer; // Ÿ���õ� ��

    public float attackPoint;//���ݷ�
    public float defensePoint; // ����
    public int monsterHealthPoint;
    public float baseMonsterAp;
    public float baseMonsterDp;
    public int baseMonsterHp;
    public float runSpeed; // �ȴ� �ӵ�
    public bool canAttack;//�Ϲ� ����
    public float attackTimer;//����


    public float attackRange = 1.0f; // ������ ������ �Ÿ�
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


        attackPoint = baseMonsterAp * stageCount;//���ݷ�
        defensePoint = baseMonsterDp * stageCount; // ����
        monsterHealthPoint = baseMonsterHp * stageCount;

        runSpeed = 3.0f; // �ȴ� �ӵ�
        canAttack = true;//�Ϲ� ����
        attackTimer = 1.5f;//����
        isBoss = false;
    }

    public void BossSetting(int stageCount) 
    {
        baseMonsterAp = 5 * SetStageMonsterStat(stageCount);
        baseMonsterDp = 5 * SetStageMonsterStat(stageCount);
        baseMonsterHp = 10 * SetStageMonsterStat(stageCount);   


        attackPoint = 2 * baseMonsterAp * stageCount;//���ݷ�
        defensePoint = 2 * baseMonsterDp * stageCount; // ����
        monsterHealthPoint = 2 *baseMonsterHp * stageCount;

        runSpeed = 3.0f; // �ȴ� �ӵ�
        canAttack = true;//�Ϲ� ����
        attackTimer = 1.0f;//����
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
