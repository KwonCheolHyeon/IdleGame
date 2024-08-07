using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void EnterState(PlayerScript character);
    void UpdateState(PlayerScript character);
    void FixedUpdateState(PlayerScript character);
    void ExitState(PlayerScript character);
}

public interface IMonsterState
{
    void EnterState(EnemyScript character);
    void UpdateState(EnemyScript character);
    void FixedUpdateState(EnemyScript character);
    void ExitState(EnemyScript character);
}