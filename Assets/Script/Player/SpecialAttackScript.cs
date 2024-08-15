using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackScript : IPlayerState
{
    private bool bHasAttacked;
    private bool bAnimationFinished;

    public void EnterState(PlayerScript character)
    {
        Debug.Log("특수공격 ON");
        character.animator.SetBool("IsSkill", true);
        bHasAttacked = false;
        bAnimationFinished = false ;
    }

    public void ExitState(PlayerScript character)
    {
        Debug.Log("특수공격 off");
        character.animator.SetBool("IsSkill", false);
    }

    public void FixedUpdateState(PlayerScript character)
    {

    }
    public void UpdateState(PlayerScript character)
    {
        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (!IsInSpecialAttackState(stateInfo))
        {
            Debug.LogError("특수공격 상태 에러");
            character.SetState(character.idleState);
            return;
        }

        if (IsAnimationFinished(stateInfo, character))
        {
            HandleAnimationFinished(character);
        }
        else if (ShouldPerformSpecialAttack(stateInfo))
        {
            PerformSpecialAttack(character);
        }
    }

    private bool IsInSpecialAttackState(AnimatorStateInfo stateInfo)
    {
        return stateInfo.IsName("5_Skill_Normal");
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

    private bool ShouldPerformSpecialAttack(AnimatorStateInfo stateInfo)
    {
        return stateInfo.normalizedTime >= 0.5f && !bHasAttacked;
    }

    private void PerformSpecialAttack(PlayerScript character)
    {
        if (character.targetEnemy != null && character.targetEnemy.gameObject.activeSelf)
        {
            Debug.Log("스킬 공격 성공");
            MonsterScript enemy = character.targetEnemy.GetComponent<MonsterScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(character.attackPoint * 2); // 적에게 2배 데미지
                character.canSpecialAttack = false;
                character.AttackCoolTime(1); // 쿨타임 설정
                bHasAttacked = true;
            }
        }
    }

}
