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
        // 현재 애니메이터의 상태 정보 가져오기
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


        // 공격이 시작된 후 한번만 공격 로직을 실행
        if (!hasAttacked && character.targetEnemy != null)
        {
            // 적에게 데미지 주기
            EnemyScript enemy = character.targetEnemy.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(character.attackPoint * 2);//적에게 데미지
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
