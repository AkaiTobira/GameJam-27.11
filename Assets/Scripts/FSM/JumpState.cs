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

        PlayerAnimator.Instance.SetBool("Jumping", true);
    }
    public void OnExit()
    {
        PlayerAnimator.Instance.SetBool("Jumping", false);
    }
    public override void HandleInput(){
        PlayerAnimator.Instance.UpdateSide((int)Input.GetAxisRaw("Horizontal"));
        if ( accelerateJumpforce ){
            PlayerDetector.Instance.AddJumpForce();
            if( !PlayerInput.isJumpHold() || elapsedTime >= Player.Instance.JumpHoldTime){
                accelerateJumpforce = false;
            }
            elapsedTime = Mathf.Min(elapsedTime + Time.deltaTime, Player.Instance.JumpHoldTime);
        }
    }

    public override void ProcessGraphics(){}
    public override void ProcessPhysics(){


        PlayerDetector.Instance.Move((int)Input.GetAxisRaw("Horizontal"));
        if( PlayerDetector.Instance.isOnGround( ) && !accelerateJumpforce){
            CameraShake.Instance.TriggerShake(0.1f);
            _stateMachine.ChangeToState( new IdleState(_gameObject));
        }

    }
}
