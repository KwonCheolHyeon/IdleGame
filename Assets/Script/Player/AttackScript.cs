using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackScript : IPlayerState
{
    private bool hasAttacked; // 한번만 공격하도록 하기 위한 플래그
    private bool animationFinished = false;
    public void EnterState(PlayerScript character)
    {
        Debug.Log("공격 ON");
        character.animator.SetBool("IsAttack", true);
        hasAttacked = false;
        animationFinished = false;
    }

    public void ExitState(PlayerScript character)
    {
        Debug.Log("공격 off");
        character.animator.SetBool("IsAttack", false);
    }

    public void FixedUpdateState(PlayerScript character)
    {
       
    }

    public void UpdateState(PlayerScript character)
    {
        // 현재 애니메이터의 상태 정보 가져오기
        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        // "2_Attack_Normal" 애니메이션이 진행 중일 때
        if (stateInfo.IsName("2_Attack_Normal"))
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
            // 애니메이션이 절반 진행되었을 때
            else if (stateInfo.normalizedTime >= 0.5f && !hasAttacked && character.targetEnemy != null)
            {
                // 적에게 데미지 주기
                EnemyScript enemy = character.targetEnemy.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    enemy.TakeDamage(character.attackPoint); // 적에게 데미지
                    character.canAttack = false;
                    character.AttackCoolTime(0);
                    hasAttacked = true;
                }
            }
        }
        else 
        {
            Debug.LogError("일반 공격 에러");
            character.SetState(character.idleState);
        }
    }

}
