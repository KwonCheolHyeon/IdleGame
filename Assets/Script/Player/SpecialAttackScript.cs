using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackScript : IPlayerState
{
    private bool hasAttacked;
    private bool animationFinished;

    public void EnterState(PlayerScript character)
    {
        Debug.Log("특수공격 ON");
        character.animator.SetBool("IsSkill", true);
        hasAttacked = false;
        animationFinished = false ;
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

        // "5_Skill_Normal" 애니메이션이 진행 중일 때
        if (stateInfo.IsName("5_Skill_Normal"))
        {
            // 애니메이션이 끝났을 때
            if (stateInfo.normalizedTime >= 1 && !character.animator.IsInTransition(0))
            {
                if (!animationFinished)
                {
                    animationFinished = true;
                    character.SetState(character.idleState);
                }
            }
            // 애니메이션이 절반 진행되었을 때 적에게 데미지를 줌
            else if (stateInfo.normalizedTime >= 0.5f && !hasAttacked && character.targetEnemy != null)
            {
                EnemyScript enemy = character.targetEnemy.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    enemy.TakeDamage(character.attackPoint * 2); // 적에게 데미지
                    character.canSpecialAttack = false;
                    character.AttackCoolTime(1);
                    hasAttacked = true;
                }
            }
        }
        else 
        {
            Debug.LogError("스킬 공격 에러");
            character.SetState(character.idleState);
        }
    }

    //private void ChangeState(PlayerScript character)
    //{
    //    character.SetState(character.idleState);
    //}
}
