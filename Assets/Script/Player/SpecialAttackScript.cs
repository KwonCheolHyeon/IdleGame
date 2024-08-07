using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackScript : IPlayerState
{
    private bool hasAttacked;
    private bool animationFinished;

    public void EnterState(PlayerScript character)
    {
        Debug.Log("Ư������ ON");
        character.animator.SetBool("IsSkill", true);
        hasAttacked = false;
        animationFinished = false ;
    }

    public void ExitState(PlayerScript character)
    {
        Debug.Log("Ư������ off");
        character.animator.SetBool("IsSkill", false);
    }

    public void FixedUpdateState(PlayerScript character)
    {

    }
    public void UpdateState(PlayerScript character)
    {
        // ���� �ִϸ������� ���� ���� ��������
        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("5_Skill_Normal") && stateInfo.normalizedTime >= 1 && !character.animator.IsInTransition(0))
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
                enemy.TakeDamage(character.attackPoint * 2);//������ ������
                character.canSpecialAttack = false;
                character.AttackCoolTime(1);
                hasAttacked = true;
            }
        }

        
       
    }

    private void ChangeState(PlayerScript character)
    {
        character.SetState(character.idleState);
    }
}
