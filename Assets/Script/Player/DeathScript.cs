using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : ICharacterState
{
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

    public void HandleInput(PlayerScript character)
    {

    }

    public void UpdateState(PlayerScript character)
    {

    }
}
