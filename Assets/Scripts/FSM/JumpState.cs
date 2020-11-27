using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState, IState
{

    bool accelerateJumpforce = true;
    float elapsedTime = 0;

    public JumpState(GameObject gameObject) : base(gameObject){}
    public void OnEnter(){
        PlayerDetector.Instance.Jump();
        accelerateJumpforce = true;
    }
    public void OnExit(){}
    public override void HandleInput(){
        Debug.Log(accelerateJumpforce);
        if( accelerateJumpforce ){
            
            if( !PlayerInput.isJumpHold() || elapsedTime >= Player.Instance.JumpHoldTime){
                accelerateJumpforce = false;
                if( Player.Instance.JumpHoldTime <= 0 ) return;
                PlayerDetector.Instance.AddJumpForce( elapsedTime/Player.Instance.JumpHoldTime );
            }
            elapsedTime = Mathf.Min(elapsedTime + Time.deltaTime, Player.Instance.JumpHoldTime);
        }
    }

    public override void ProcessGraphics(){}
    public override void ProcessPhysics(){


        PlayerDetector.Instance.Move((int)Input.GetAxisRaw("Horizontal") * new Vector2(3, 0) );
        if( PlayerDetector.Instance.CheckGround( ) && !accelerateJumpforce){
            _stateMachine.ChangeToState( new IdleState(_gameObject));
        }

    }
}
