using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMonsterDeath : IMonsterState
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

                // �θ� ������Ʈ�� ��Ȱ��ȭ
                if (character.gameObject.transform.parent != null)
                {
                    character.DeathMonster();
                    character.DestroyBoss();
                }

            }
        }
    }
}
