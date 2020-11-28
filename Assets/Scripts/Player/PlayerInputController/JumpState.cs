using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState, IState
{

    bool accelerateJumpforce = true;
    float elapsedTime = 0;

    Rigidbody2D _playerRB;

    public JumpState(Entity gameObject) : base(gameObject){}
    public void OnEnter(){
        PlayerDetector.Instance.Jump();
        accelerateJumpforce = true;
        _playerRB = PlayerDetector.Instance.GetComponent<Rigidbody2D>();

        PlayerAnimator.AnimatorInstance.SetBool("Jumping", true);
        PlayerAnimator.AnimatorInstance.SetBool("OnGround", false);
    }

    public void OnExit()
    {
        PlayerAnimator.AnimatorInstance.SetBool("OnGround", true);
        PlayerAnimator.AnimatorInstance.SetBool("Jumping", false);
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

    public override void ProcessGraphics(){
        PlayerAnimator.AnimatorInstance.SetFloat("JumpDirection", Mathf.Sign(_playerRB.velocity.y));
        PlayerAnimator.AnimatorInstance.SetBool("Moving", PlayerInput.isLeftHold() || PlayerInput.isRightHold());
        if( _playerRB.velocity.y > 0 ){
            PlayerAnimator.AnimatorInstance.SetBool("OnGround", false);
        }
    }
    
    public override void ProcessPhysics(){
        PlayerDetector.Instance.Move((int)Input.GetAxisRaw("Horizontal"));
        if( PlayerDetector.Instance.isOnGround( ) && !accelerateJumpforce){
            CameraShake.Instance.TriggerShake(0.1f);
            _stateMachine.ChangeToState( new IdleState(_entity));
        }
    }
}
