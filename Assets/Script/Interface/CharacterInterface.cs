using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState
{
    void EnterState(PlayerScript character);
    void UpdateState(PlayerScript character);
    void FixedUpdateState(PlayerScript character);
    void ExitState(PlayerScript character);
}