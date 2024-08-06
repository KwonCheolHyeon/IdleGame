using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : ICharacterState
{
    public void EnterState(PlayerScript character)
    {
        character.animator.SetBool("IsAttack", true);
    }

    public void ExitState(PlayerScript character)
    {
        character.animator.SetBool("IsAttack", false);
    }

    public void FixedUpdateState(PlayerScript character)
    {

    }

    public void UpdateState(PlayerScript character)
    {

    }
}
