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
    public Animator animator; // Animator �߰�
    public Transform targetEnemy; // Ÿ���õ� ��

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
        nowHealthPoint -= (int)Mathf.Max(0, damage - defensePoint); // ���� ����
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
        defensePoint = 5.0f; // �⺻ ���� ����
        maxHealthPoint = 1000;
        nowHealthPoint = maxHealthPoint;
        attackRange = 1.0f;
    }

    public void PlayerLevelUp(int type) 
    {
        const float upgradeAmount = 10.0f; // ���׷��̵� �� �����ϴ� ��

        switch (type)
        {
            case 0: // ���ݷ� ������
                attackPoint += upgradeAmount;
                Debug.Log("���ݷ��� " + attackPoint + "�� �����߽��ϴ�.");
                break;
            case 1: // ���� ������
                defensePoint += upgradeAmount;
                Debug.Log("������ " + defensePoint + "�� �����߽��ϴ�.");
                break;
            case 2: // ü�� ������
                maxHealthPoint += (int)upgradeAmount * 10; // ü���� �� ũ�� ����
                Debug.Log("ü���� " + maxHealthPoint + "�� �����߽��ϴ�.");
                break;
            default:
                Debug.LogWarning("������ ����");
                break;
        }
    }
}
