using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackScript : IMonsterState
{
    private bool hasAttacked; // 한번만 공격하도록 하기 위한 플래그
    private bool animationFinished = false;
    public void EnterState(EnemyScript character)
    {
        Debug.Log("공격 ON");
        character.animator.SetBool("IsAttack", true);
        hasAttacked = false; // 공격 상태에 들어갈 때 플래그 초기화
    }

    public void ExitState(EnemyScript character)
    {
        Debug.Log("공격 off");
        character.animator.SetBool("IsAttack", false);
    }

    public void FixedUpdateState(EnemyScript character)
    {
        
    }

    public void UpdateState(EnemyScript character)
    {
        // 현재 애니메이터의 상태 정보 가져오기
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


        // 공격이 시작된 후 한번만 공격 로직을 실행
        if (!hasAttacked && character.targetPlayer != null)
        {
            // 적에게 데미지 주기
            PlayerScript player = character.targetPlayer.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.TakeDamage(character.attackPoint);//적에게 데미지
                character.canAttack = false;
                character.AttackCoolTime();
                hasAttacked = true;
            }
        }
    }

    private void ChangeState(EnemyScript character)
    {
        character.SetState(character.idleState);
    }
}
