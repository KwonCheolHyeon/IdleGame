using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathScript : IMonsterState
{
    private bool animationFinished;
    public void EnterState(EnemyScript character)
    {
        character.animator.SetTrigger("DeathTrigger");
        animationFinished = false;
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

                // 부모 오브젝트를 비활성화
                if (character.gameObject.transform.parent != null)
                {
                    character.gameObject.SetActive(false);
                    character.gameObject.transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    // 부모 오브젝트가 없는 경우 자기 자신을 비활성화
                    character.gameObject.SetActive(false);
                }
            }
        }
    }
}
