using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseState
{
    protected GameObject _gameObject;
    protected FSM _stateMachine;
    
    public BaseState(GameObject gameObject)
    {
        _gameObject = gameObject;
        
    }
    public void SetFSM(FSM stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public void Update()
    {
        HandleInput();
        ProcessGraphics();
    }

    public void FixedUpdate()
    {
        ProcessPhysics();
    }

    public abstract void HandleInput();
    public abstract void ProcessPhysics();
    public abstract void ProcessGraphics();

}

