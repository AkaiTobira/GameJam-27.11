using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState, IState
{
    public IdleState(Entity gameObject) : base(gameObject){}
    public void OnEnter(){
    }
    public void OnExit(){}
    public override void HandleInput(){
         if( PlayerInput.isJumpPressed()){
            _stateMachine.ChangeToState( new JumpState(_entity));
        }else  if( PlayerInput.isLeftHold() || PlayerInput.isRightHold() ){
            _stateMachine.ChangeToState( new MoveState(_entity));
        }
    }
    public override void ProcessGraphics(){
        PlayerAnimator.AnimatorInstance.SetBool("OnGround", true);
    }
    public override void ProcessPhysics(){}
}
