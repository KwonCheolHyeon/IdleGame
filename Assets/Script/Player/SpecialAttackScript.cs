using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackScript : ICharacterState
{
    public void EnterState(PlayerScript character)
    {
        character.animator.SetBool("IsSkill", true);
    }

    public void ExitState(PlayerScript character)
    {
        character.animator.SetBool("IsSkill", false);
    }

    public void FixedUpdateState(PlayerScript character)
    {

    }
    public void UpdateState(PlayerScript character)
    {

    }
}
