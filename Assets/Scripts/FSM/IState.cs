using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnExit();
    void OnEnter();
    void Update();
    void FixedUpdate();
    void SetFSM(FSM stateMachine);
}
