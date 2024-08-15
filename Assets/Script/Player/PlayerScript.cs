using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerScript : MonoBehaviour
{
    public IPlayerState currentState { get; private set; }
    public IPlayerState idleState = new IdleScript();
    public IPlayerState runState = new RunScript();
    public IPlayerState attackState = new AttackScript();
    public IPlayerState specialAttackState = new SpecialAttackScript();
    public IPlayerState deathState = new DeathScript();

    public Vector2 inputVec;
    public Rigidbody2D rigid;
    public Animator animator; // Animator 추가
    public Transform targetEnemy; // 타겟팅된 적

    public float attackPoint { get; private set; }
    public float defensePoint { get; private set; }
    public int maxHealthPoint { get; private set; }
    public int nowHealthPoint { get; private set; }
    public float runSpeed { get; private set; }
    public bool canSpecialAttack { get; set; }
    public float specialAttackTimer { get; private set; }
    public bool canAttack { get; set; }
    public float attackTimer { get; private set; }
    public float attackRange { get; private set; }

    private bool isAttackCooldownActive = false;
    private bool isSpecialAttackCooldownActive = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        SettingPlayer();
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

    public void SetState(IPlayerState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }
        currentState = newState;
        currentState.EnterState(this);
    }

    public void TakeDamage(float damage) 
    {
        nowHealthPoint -= (int)Mathf.Max(0, damage - defensePoint); // 방어력 적용
        if (nowHealthPoint <= 0)
        {
            SetState(deathState);
        }
    }

    public void AttackCoolTime(int type)
    {
        if (type == 0 && !isAttackCooldownActive)
        {
            StartCoroutine(CooldownCoroutine(attackTimer, () => canAttack = true, () => isAttackCooldownActive = false));
        }
        else if (type == 1 && !isSpecialAttackCooldownActive)
        {
            StartCoroutine(CooldownCoroutine(specialAttackTimer, () => canSpecialAttack = true, () => isSpecialAttackCooldownActive = false));
        }
    }

    private IEnumerator CooldownCoroutine(float cooldownTime, System.Action onCooldownEnd, System.Action onCooldownComplete)
    {
        onCooldownComplete();
        yield return new WaitForSeconds(cooldownTime);
        onCooldownEnd();
    }

    private void SettingPlayer() 
    {
        runSpeed = 3.0f;
        specialAttackTimer = 5.0f;
        canSpecialAttack = true;
        attackTimer = 1.0f;
        canAttack = true;
        attackPoint = 20.0f;
        defensePoint = 5.0f; // 기본 방어력 설정
        maxHealthPoint = 1000;
        nowHealthPoint = maxHealthPoint;
        attackRange = 1.0f;
    }

    public void PlayerLevelUp(int type) 
    {
        const float upgradeAmount = 10.0f; // 업그레이드 시 증가하는 양

        switch (type)
        {
            case 0: // 공격력 레벨업
                attackPoint += upgradeAmount;
                Debug.Log("공격력이 " + attackPoint + "로 증가했습니다.");
                break;
            case 1: // 방어력 레벨업
                defensePoint += upgradeAmount;
                Debug.Log("방어력이 " + defensePoint + "로 증가했습니다.");
                break;
            case 2: // 체력 레벨업
                maxHealthPoint += (int)upgradeAmount * 10; // 체력은 더 크게 증가
                Debug.Log("체력이 " + maxHealthPoint + "로 증가했습니다.");
                break;
            default:
                Debug.LogWarning("레벨업 오류");
                break;
        }
    }
}
