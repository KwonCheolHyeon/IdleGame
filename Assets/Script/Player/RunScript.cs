using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunScript : ICharacterState
{
    public void EnterState(PlayerScript character)
    {
        character.animator.SetBool("IsRun", true);
    }

    public void ExitState(PlayerScript character)
    {
        character.animator.SetBool("IsRun", false);
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
