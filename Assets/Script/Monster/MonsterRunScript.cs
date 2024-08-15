using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRunScript : IMonsterState
{
    public void EnterState(MonsterScript character)
    {
        character.animator.SetBool("IsRun", true);
    }

    public void ExitState(MonsterScript character)
    {
        character.animator.SetBool("IsRun", false);
    }

    public void FixedUpdateState(MonsterScript character)
    {
        
    }

    public void UpdateState(MonsterScript character)
    {
        if (character.targetPlayer == null)
        {
            character.SetState(character.idleState);
            return;
        }

        float distanceToEnemy = Vector3.Distance(character.transform.position, character.targetPlayer.position);

        if (distanceToEnemy <= character.attackRange)
        {
            character.SetState(character.idleState);
            return;
        }


        Vector3 direction = (character.targetPlayer.position - character.transform.position).normalized;
        character.transform.position += direction * character.runSpeed * Time.deltaTime;

        Vector2 dir2d = new Vector2(direction.x, direction.y);

        if (dir2d.x < 0)
        {
            character.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dir2d.x > 0)
        {
            character.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
