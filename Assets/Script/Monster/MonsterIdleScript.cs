using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleScript : IMonsterState
{
    public void EnterState(EnemyScript character)
    {
       
    }

    public void ExitState(EnemyScript character)
    {
        
    }

    public void FixedUpdateState(EnemyScript character)
    {

    }

    public void UpdateState(EnemyScript character)
    {

        if (character.targetPlayer != null)
        {
            float distanceToEnemy = Vector3.Distance(character.transform.position, character.targetPlayer.position);

            if (distanceToEnemy <= character.attackRange)
            {
                if (character.canAttack)
                {
                    character.SetState(character.attackState);
                    return;
                }
            }
        }
        else
        {
            FindPlayer(character);
        }
    }

    public void FindPlayer(EnemyScript character)
    {
        // targetEnemy가 이미 존재하면 FindTarget을 실행하지 않도록 함
        if (character.targetPlayer != null)
        {
            Debug.Log($"이미 타겟이 설정되어 있습니다: {character.targetPlayer.name}");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Transform closestEnemy = player.transform;
            character.targetPlayer = closestEnemy; // 가장 가까운 적을 타겟으로 설정
            Debug.Log($"타겟 발견: {character.targetPlayer.name}");
            character.SetState(character.runState);
            return;
        }
        else
        {
            Debug.Log("적 없음");
            return;
        }
    }

  
}
