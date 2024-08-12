using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackScript : IMonsterState
{
    private bool hasAttacked; // �ѹ��� �����ϵ��� �ϱ� ���� �÷���
    private bool animationFinished = false;
    public void EnterState(EnemyScript character)
    {
        Debug.Log("���� ON");
        character.animator.SetBool("IsAttack", true);
        hasAttacked = false; // ���� ���¿� �� �� �÷��� �ʱ�ȭ
    }

    public void ExitState(EnemyScript character)
    {
        Debug.Log("���� off");
        character.animator.SetBool("IsAttack", false);
    }

    public void FixedUpdateState(EnemyScript character)
    {
        
    }

    public void UpdateState(EnemyScript character)
    {
        // ���� �ִϸ������� ���� ���� ��������
        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("2_Attack_Normal") && stateInfo.normalizedTime >= 1 && !character.animator.IsInTransition(0))
        {
            if (!animationFinished)
            {
                animationFinished = true;
                ChangeState(character);
            }
        }
        else
        {
            animationFinished = false;
        }


        // ������ ���۵� �� �ѹ��� ���� ������ ����
        if (!hasAttacked && character.targetPlayer != null)
        {
            // ������ ������ �ֱ�
            PlayerScript player = character.targetPlayer.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.TakeDamage(character.attackPoint);//������ ������
                character.canAttack = false;
                character.AttackCoolTime();
                hasAttacked = true;
            }
        }
    }

    private void ChangeState(EnemyScript character)
    {
        character.SetState(character.idleState);
    }
}
