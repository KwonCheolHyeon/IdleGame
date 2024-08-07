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
    public Animator animator; // Animator �߰�
    public Transform targetEnemy; // Ÿ���õ� ��

    public float attackPoint;//���ݷ�
    public float defensePoint; // ����
    public float healthPoint; // ü��
    public float runSpeed; // �ȴ� �ӵ�
    public bool canSpecialAttack;// ��ų ����
    public float specialAttackTimer; // ��ų���� ��Ÿ��
    public bool canAttack;//�Ϲ� ����
    public float attackTimer;//����


    public float attackRange = 1.0f; // ������ ������ �Ÿ�
    private bool isAttackCooldownActive = false;
    private bool isSpecialAttackCooldownActive = false;
    //public bool canHit;//������ �޴� ��Ÿ��
    //public float hitTimer;//������ ��Ÿ��
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        runSpeed = 3.0f;
        specialAttackTimer = 5.0f;
        canSpecialAttack = true;
        attackTimer = 1.0f;
        canAttack = true;
        attackPoint = 2.0f;
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

    public void TakeDamage(float attackPoint) 
    {
        
    }

    public void AttackCoolTime(int type)
    {
        if (type == 0 && !isAttackCooldownActive) // Normal Attack
        {
            StartCoroutine(CooldownCoroutine(attackTimer, () => canAttack = true, () => isAttackCooldownActive = false));
        }
        else if (type == 1 && !isSpecialAttackCooldownActive) // Special Attack
        {
            StartCoroutine(CooldownCoroutine(specialAttackTimer, () => canSpecialAttack = true, () => isSpecialAttackCooldownActive = false));
        }
    }

    private IEnumerator CooldownCoroutine(float cooldownTime, System.Action onCooldownEnd, System.Action onCooldownComplete)
    {
        onCooldownComplete(); // Mark cooldown as active
        yield return new WaitForSeconds(cooldownTime);
        onCooldownEnd();
        onCooldownComplete(); // Mark cooldown as complete
    }

    //private IEnumerator HitCoolTime() 
    //{
    //    yield return new WaitForSeconds(hitTimer);
    //}

}
