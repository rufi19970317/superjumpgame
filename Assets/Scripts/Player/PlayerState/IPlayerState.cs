using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void Enter();
    void Exit();
    void HandleMoveInput(int dir);
    void HandleJumpInput();
    void Update();
}
