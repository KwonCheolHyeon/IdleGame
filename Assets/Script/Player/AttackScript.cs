using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackScript : IPlayerState
{
    private bool bHasAttacked; // �ѹ��� �����ϵ��� �ϱ� ���� �÷���
    private bool bAnimationFinished = false;
    public void EnterState(PlayerScript character)
    {
        Debug.Log("���� ON");
        character.animator.SetBool("IsAttack", true);
        bHasAttacked = false;
        bAnimationFinished = false;
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
        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (!IsInAttackState(stateInfo))
        {
            Debug.LogError("���� ���� ����");
            character.SetState(character.idleState);
            return;
        }

        if (IsAnimationFinished(stateInfo, character))
        {
            HandleAnimationFinished(character);
        }
        else if (ShouldAttack(stateInfo))
        {
            PerformAttack(character);
        }
    }

    private bool IsInAttackState(AnimatorStateInfo stateInfo)
    {
        return stateInfo.IsName("2_Attack_Normal");
    }

    private bool IsAnimationFinished(AnimatorStateInfo stateInfo, PlayerScript character)
    {
        return stateInfo.normalizedTime >= 1 && !character.animator.IsInTransition(0);
    }

    private void HandleAnimationFinished(PlayerScript character)
    {
        if (!bAnimationFinished)
        {
            bAnimationFinished = true;
            character.SetState(character.idleState);
        }
    }

    private bool ShouldAttack(AnimatorStateInfo stateInfo)
    {
        return stateInfo.normalizedTime >= 0.5f && !bHasAttacked;
    }

    private void PerformAttack(PlayerScript character)
    {
        if (character.targetEnemy != null && character.targetEnemy.gameObject.activeSelf)
        {
            Debug.Log("���� ����");
            MonsterScript enemy = character.targetEnemy.GetComponent<MonsterScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(character.attackPoint);
                character.canAttack = false;
                character.AttackCoolTime(0);
                bHasAttacked = true;
            }
        }
    }
}
