using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleScript : ICharacterState
{
    

    public void EnterState(PlayerScript character)
    {
        character.targetEnemy = null;
    }

    public void ExitState(PlayerScript character)
    {

    }

    public void FixedUpdateState(PlayerScript character)
    {

    }

    public void HandleInput(PlayerScript character)
    {

    }

    public void UpdateState(PlayerScript character)
    {
        FindTarget(character);
    }


    public void FindTarget(PlayerScript character) 
    {
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

            character.SetState(character.runState);
            return;
        }
        else 
        {
            return;
        }
       
    }
}
