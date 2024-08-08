using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RunScript : IPlayerState
{
    public void EnterState(PlayerScript character)
    {
        Debug.Log("달리기 ON");
        character.animator.SetBool("IsRun", true);
    }

    public void ExitState(PlayerScript character)
    {
        Debug.Log("달리기 OFF");
        character.animator.SetBool("IsRun", false);
        character.inputVec = Vector2.zero;
    }
    public void UpdateState(PlayerScript character)
    {
        if (character.targetEnemy == null || !character.targetEnemy.gameObject.activeSelf)
        {
            character.SetState(character.idleState);
            return;
        }

        CheckStopForAttack(character);

        MoveTowardsEnemy(character);
    }
    public void FixedUpdateState(PlayerScript character)
    {
        
    }

    private void CheckStopForAttack(PlayerScript character) 
    {

        float distanceToEnemy = Vector3.Distance(character.transform.position, character.targetEnemy.position);

        if (distanceToEnemy <= character.attackRange)
        {
            character.SetState(character.idleState);
            return;
        }
    }
    private void MoveTowardsEnemy(PlayerScript character)
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
