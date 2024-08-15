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
    void EnterState(MonsterScript character);
    void UpdateState(MonsterScript character);
    void FixedUpdateState(MonsterScript character);
    void ExitState(MonsterScript character);
}