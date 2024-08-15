using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MonsterAttackScript : IMonsterState
{
    private bool bHasAttacked;
    private bool bAnimationFinished;

    public void EnterState(MonsterScript character)
    {
        Debug.Log("공격 ON");
        character.animator.SetBool("IsAttack", true);
        bHasAttacked = false;
        bAnimationFinished = false;
    }

    public void ExitState(MonsterScript character)
    {
        Debug.Log("공격 OFF");
        character.animator.SetBool("IsAttack", false);
    }

    public void FixedUpdateState(MonsterScript character)
    {
        // 필요 시 물리 관련 업데이트 코드 추가
    }

    public void UpdateState(MonsterScript character)
    {
        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (!IsInAttackState(stateInfo))
        {
            bAnimationFinished = false;
            return;
        }

        if (IsAnimationFinished(stateInfo, character))
        {
            HandleAnimationFinished(character);
        }

        if (ShouldAttack(character))
        {
            PerformAttack(character);
        }
    }

    private bool IsInAttackState(AnimatorStateInfo stateInfo)
    {
        return stateInfo.IsName("2_Attack_Normal");
    }

    private bool IsAnimationFinished(AnimatorStateInfo stateInfo, MonsterScript character)
    {
        return stateInfo.normalizedTime >= 1 && !character.animator.IsInTransition(0);
    }

    private void HandleAnimationFinished(MonsterScript character)
    {
        if (!bAnimationFinished)
        {
            bAnimationFinished = true;
            ChangeState(character);
        }
    }

    private bool ShouldAttack(MonsterScript character)
    {
        return !bHasAttacked && character.targetPlayer != null;
    }

    private void PerformAttack(MonsterScript character)
    {
        PlayerScript player = character.targetPlayer.GetComponent<PlayerScript>();
        if (player != null)
        {
            Debug.Log("공격 성공");
            player.TakeDamage(character.attackPoint);
            character.canAttack = false;
            character.AttackCoolTime();
            bHasAttacked = true;
        }
    }

    private void ChangeState(MonsterScript character)
    {
        character.SetState(character.idleState);
    }
}
