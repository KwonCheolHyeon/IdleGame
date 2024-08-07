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
        // targetEnemy�� �̹� �����ϸ� FindTarget�� �������� �ʵ��� ��
        if (character.targetPlayer != null)
        {
            Debug.Log($"�̹� Ÿ���� �����Ǿ� �ֽ��ϴ�: {character.targetPlayer.name}");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Transform closestEnemy = player.transform;
            character.targetPlayer = closestEnemy; // ���� ����� ���� Ÿ������ ����
            Debug.Log($"Ÿ�� �߰�: {character.targetPlayer.name}");
            character.SetState(character.runState);
            return;
        }
        else
        {
            Debug.Log("�� ����");
            return;
        }
    }

  
}
