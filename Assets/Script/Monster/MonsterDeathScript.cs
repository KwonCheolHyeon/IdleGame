using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathScript : IMonsterState
{
    private bool animationFinished = false;
    public void EnterState(EnemyScript character)
    {
        character.animator.SetTrigger("DeathTrigger");
    }

    public void ExitState(EnemyScript character)
    {
        
    }

    public void FixedUpdateState(EnemyScript character)
    {
        
    }

    public void UpdateState(EnemyScript character)
    {
        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("4_Death") && stateInfo.normalizedTime >= 1 && !character.animator.IsInTransition(0))
        {
            if (!animationFinished)
            {
                animationFinished = true;
                character.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
