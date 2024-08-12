using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackScript : IPlayerState
{
    private bool bHasAttacked;
    private bool bAnimationFinished;

    public void EnterState(PlayerScript character)
    {
        Debug.Log("Ư������ ON");
        character.animator.SetBool("IsSkill", true);
        bHasAttacked = false;
        bAnimationFinished = false ;
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

        if (stateInfo.IsName("5_Skill_Normal"))
        {
            // �ִϸ��̼��� ������ ��
            if (stateInfo.normalizedTime >= 1 && !character.animator.IsInTransition(0))
            {
                if (!bAnimationFinished)
                {
                    bAnimationFinished = true;
                    character.SetState(character.idleState);
                }
            }
            else if (stateInfo.normalizedTime >= 0.5f && !bHasAttacked)
            {
                if (character.targetEnemy != null && character.targetEnemy.gameObject.activeSelf)
                {
                    Debug.Log("��ų ���� ����");
                    EnemyScript enemy = character.targetEnemy.GetComponent<EnemyScript>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(character.attackPoint * 2); // ������ ������
                        character.canSpecialAttack = false;
                        character.AttackCoolTime(1);
                        bHasAttacked = true;
                    }
                }
            }
        }
    }

    //private void ChangeState(PlayerScript character)
    //{
    //    character.SetState(character.idleState);
    //}
}
