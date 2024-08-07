using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleScript : IPlayerState
{
    public void EnterState(PlayerScript character)
    {
        Debug.Log("iDLE STATE ����");
        
    }

    public void ExitState(PlayerScript character)
    {
        Debug.Log("iDLE STATE �r��");
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
        // targetEnemy�� �̹� �����ϸ� FindTarget�� �������� �ʵ��� ��
        if (character.targetEnemy != null)
        {
            Debug.Log($"�̹� Ÿ���� �����Ǿ� �ֽ��ϴ�: {character.targetEnemy.name}");
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

            character.targetEnemy = closestEnemy; // ���� ����� ���� Ÿ������ ����
            Debug.Log($"Ÿ�� �߰�: {character.targetEnemy.name}");
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
