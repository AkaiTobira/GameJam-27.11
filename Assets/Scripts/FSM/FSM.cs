using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public IState _state;

    public FSM(IState state)
    {
        _state = state;
        _state.SetFSM(this);
    }

   public void Update()
    {
        _state.Update();
    }

    public void FixedUpdate()
    {
        _state.FixedUpdate();
    }

    public void OnNotify(GameEvent newEvent)
    {

    }

    public void ChangeToState(IState newState)
    {
        newState.SetFSM(this);
        _state.OnExit();
        _state = newState;
        _state.OnEnter();
    }
}

