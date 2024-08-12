using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : IPlayerState
{
    private bool animationFinished = false;
    public void EnterState(PlayerScript character)
    {
        character.animator.SetTrigger("DeathTrigger");
    }
    public void ExitState(PlayerScript character)
    {

    }

    public void FixedUpdateState(PlayerScript character)
    {

    }


    public void UpdateState(PlayerScript character)
    {
        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("4_Death") && stateInfo.normalizedTime >= 1 && !character.animator.IsInTransition(0))
        {
            if (!animationFinished)
            {
                animationFinished = true;

                character.gameObject.SetActive(false);
            }
        }
    }


}
