using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseState
{
    protected Entity _entity;
    protected FSM _stateMachine;
    
    public BaseState(Entity entity)
    {
        _entity = entity;
        
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

