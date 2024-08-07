using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackScript : ICharacterState
{
    private bool hasAttacked; // �ѹ��� �����ϵ��� �ϱ� ���� �÷���
    private bool animationFinished = false;
    public void EnterState(PlayerScript character)
    {
        Debug.Log("���� ON");
        character.animator.SetBool("IsAttack", true);
        hasAttacked = false; // ���� ���¿� �� �� �÷��� �ʱ�ȭ
    }

    public void ExitState(PlayerScript character)
    {
        Debug.Log("���� off");
        character.animator.SetBool("IsAttack", false);
    }

    public void FixedUpdateState(PlayerScript character)
    {
       
    }

    public void UpdateState(PlayerScript character)
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
        if (!hasAttacked && character.targetEnemy != null)
        {
            // ������ ������ �ֱ�
            EnemyScript enemy = character.targetEnemy.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(character.attackPoint);//������ ������
                character.canAttack = false;
                character.AttackCoolTime(0);
                hasAttacked = true;
            }
        }

       
    }

    private void ChangeState(PlayerScript character) 
    {
        character.SetState(character.idleState);
    }



}
