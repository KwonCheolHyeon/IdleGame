using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MonsterScript : MonoBehaviour
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

    public float attackPoint { get; private set; }
    public float defensePoint { get; private set; }
    public int monsterHealthPoint { get; private set; }
    public float baseMonsterAp { get; private set; }
    public float baseMonsterDp { get; private set; }
    public int baseMonsterHp { get; private set; }
    public float runSpeed { get; private set; }
    public bool canAttack { get; set; }
    public float attackTimer { get; private set; }
    public float attackRange { get; private set; }

    private bool isAttackCooldownActive = false;
    private bool isBoss = false;
    private int mStage;
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
        monsterHealthPoint -= Mathf.Max(0, (int)(attackPoint - defensePoint));
        if (monsterHealthPoint <= 0)
        {
            SetState(isBoss ? bossDeathState : deathState);
        }
    }

    public void AttackCoolTime()
    {
        if (!isAttackCooldownActive)
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

    public void ConfigureMonster(int stage, bool boss = false)
    {
        int statMultiplier = (stage + 4) / 5;

        float baseAttack = 5 * statMultiplier;
        float baseDefense = 5 * statMultiplier;
        int baseHealth = 10 * statMultiplier;

        attackPoint = (boss ? 2 : 1) * baseAttack * stage;
        defensePoint = (boss ? 2 : 1) * baseDefense * stage;
        monsterHealthPoint = (boss ? 2 : 1) * baseHealth * stage;

        runSpeed = 3.0f;
        canAttack = true;
        attackTimer = boss ? 1.0f : 1.5f;
        attackRange = 0.8f;
        isBoss = boss;
        mStage = stage;
    }

    public void DestroyBoss()
    {
        GameManager.Instance.MonsterStageUp();
        MonsterManager.Instance.bBossSpawnOn = false;
        Destroy(transform.parent.gameObject);
    }

    public void DeathMonster() 
    {
        int money = 100 + (mStage);
        GameManager.Instance.EarnMoney(money);
    }

}
