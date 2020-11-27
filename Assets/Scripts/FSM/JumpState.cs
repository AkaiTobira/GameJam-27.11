using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState, IState
{
    public JumpState(GameObject gameObject) : base(gameObject){}
    public void OnEnter(){
        PlayerDetector.Instance.Jump();
    }
    public void OnExit(){}
    public override void HandleInput(){}
    public override void ProcessGraphics(){}
    public override void ProcessPhysics(){


        PlayerDetector.Instance.Move((int)Input.GetAxisRaw("Horizontal") * new Vector2(3, 0) );
        if( PlayerDetector.Instance.CheckGround( )){
            _stateMachine.ChangeToState( new IdleState(_gameObject));
        }

    }
}
