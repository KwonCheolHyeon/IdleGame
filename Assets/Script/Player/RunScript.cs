using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (character.targetEnemy == null)
        {
            character.SetState(character.idleState);
            return;
        }

        float distanceToEnemy = Vector3.Distance(character.transform.position, character.targetEnemy.position);

        if (distanceToEnemy <= character.attackRange)
        {
            character.SetState(character.idleState);
            return;
        }

        
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
    public void FixedUpdateState(PlayerScript character)
    {
        
    }

}
