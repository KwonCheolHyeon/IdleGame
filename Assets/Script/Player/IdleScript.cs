using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleScript : IPlayerState
{
    public void EnterState(PlayerScript character)
    {
        Debug.Log("iDLE STATE 진입");
        
    }

    public void ExitState(PlayerScript character)
    {
        Debug.Log("iDLE STATE 탍출");
    }

    public void FixedUpdateState(PlayerScript character)
    {

    }

    public void UpdateState(PlayerScript character)
    {

        if (character.targetEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(character.transform.position, character.targetEnemy.position);

            if (distanceToEnemy <= character.attackRange)
            {
                if (character.canSpecialAttack)
                {
                    character.SetState(character.specialAttackState);
                    return;
                }
                else if (character.canAttack)
                {
                    character.SetState(character.attackState);
                    return;
                }
            }
        }
        else
        {
            FindTarget(character);
        }
    }

    public void FindTarget(PlayerScript character)
    {
        // targetEnemy가 이미 존재하면 FindTarget을 실행하지 않도록 함
        if (character.targetEnemy != null)
        {
            Debug.Log($"이미 타겟이 설정되어 있습니다: {character.targetEnemy.name}");
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            Transform closestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(character.transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy.transform;
                }
            }

            character.targetEnemy = closestEnemy; // 가장 가까운 적을 타겟으로 설정
            Debug.Log($"타겟 발견: {character.targetEnemy.name}");
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
