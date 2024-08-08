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
        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        // "5_Skill_Normal" �ִϸ��̼��� ���� ���� ��
        if (stateInfo.IsName("5_Skill_Normal"))
        {
            // �ִϸ��̼��� ������ ��
            if (stateInfo.normalizedTime >= 1 && !character.animator.IsInTransition(0))
            {
                if (!animationFinished)
                {
                    animationFinished = true;
                    character.SetState(character.idleState);
                }
            }
            // �ִϸ��̼��� ���� ����Ǿ��� �� ������ �������� ��
            else if (stateInfo.normalizedTime >= 0.5f && !hasAttacked && character.targetEnemy != null)
            {
                EnemyScript enemy = character.targetEnemy.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    enemy.TakeDamage(character.attackPoint * 2); // ������ ������
                    character.canSpecialAttack = false;
                    character.AttackCoolTime(1);
                    hasAttacked = true;
                }
            }
        }
        else 
        {
            Debug.LogError("��ų ���� ����");
            character.SetState(character.idleState);
        }
    }

    //private void ChangeState(PlayerScript character)
    //{
    //    character.SetState(character.idleState);
    //}
}
