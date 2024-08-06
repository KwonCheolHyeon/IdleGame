using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunScript : ICharacterState
{
    private float attackRange = 1.0f; // 공격을 시작할 거리

    public void EnterState(PlayerScript character)
    {
        character.animator.SetBool("IsRun", true);
        if (character.targetEnemy == null)
        {
            character.SetState(character.idleState);
            return;
        }
    }

    public void ExitState(PlayerScript character)
    {
        character.animator.SetBool("IsRun", false);
    }
    public void UpdateState(PlayerScript character)
    {
        if (character.targetEnemy == null)
        {
            character.SetState(character.idleState);
            return;
        }

        float distanceToEnemy = Vector3.Distance(character.transform.position, character.targetEnemy.position);

        if (distanceToEnemy <= attackRange)
        {
            // 공격 범위 내에 적이 있을 때 상태를 공격 상태로 전환
            character.SetState(character.attackState);
        }
    }
    public void FixedUpdateState(PlayerScript character)
    {
        if (character.targetEnemy != null)
        {
            Vector3 direction = (character.targetEnemy.position - character.transform.position).normalized;
            character.transform.position += direction * character.runSpeed * Time.deltaTime;
        }
    }

}
