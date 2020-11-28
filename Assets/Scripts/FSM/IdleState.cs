using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState, IState
{
    public IdleState(GameObject gameObject) : base(gameObject){}
    public void OnEnter(){
    }
    public void OnExit(){}
    public override void HandleInput(){
         if( PlayerInput.isJumpPressed()){
            _stateMachine.ChangeToState( new JumpState(_gameObject));
        }else  if( PlayerInput.isLeftHold() || PlayerInput.isRightHold() ){
            _stateMachine.ChangeToState( new MoveState(_gameObject));
        }
    }
    public override void ProcessGraphics(){}
    public override void ProcessPhysics(){}
}
