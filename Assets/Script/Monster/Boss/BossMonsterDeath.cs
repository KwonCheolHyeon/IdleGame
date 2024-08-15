using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMonsterDeath : IMonsterState
{
    private bool animationFinished;
    public void EnterState(MonsterScript character)
    {
        character.animator.SetTrigger("DeathTrigger");
        animationFinished = false;
    }

    public void ExitState(MonsterScript character)
    {
       
    }

    public void FixedUpdateState(MonsterScript character)
    {
       
    }

    public void UpdateState(MonsterScript character)
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
                    character.DeathMonster();
                    character.DestroyBoss();
                }

            }
        }
    }
}
