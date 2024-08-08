using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
            if (!character.targetEnemy.gameObject.activeSelf)
            {
                character.targetEnemy = null; 
                FindTarget(character);
                return;
            }

            FlipCharacter(character);

            TryAttackEnemy(character);
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

            if (closestEnemy != null)
            {
                character.targetEnemy = closestEnemy; // ���� ����� ���� Ÿ������ ����
                Debug.Log($"Ÿ�� �߰�: {character.targetEnemy.name}");
                character.SetState(character.runState);
            }
        }
        else
        {
            Debug.Log("�� ����");
        }
    }

    private void TryAttackEnemy(PlayerScript character) 
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

    private void FlipCharacter(PlayerScript character) 
    {
        Vector3 direction = (character.targetEnemy.position - character.transform.position).normalized;
        character.transform.position += direction * character.runSpeed * Time.deltaTime;

        character.inputVec = new Vector2(direction.x, direction.y);

        if (character.inputVec.x < 0)
        {
            character.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (character.inputVec.x > 0)
        {
            character.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
