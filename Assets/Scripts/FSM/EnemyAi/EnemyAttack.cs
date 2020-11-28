using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttack : BaseState, IState
{
    float waitDuration = 0.8f;

    bool shouldJump = true;

    public EnemyAttack(Entity gameObject) : base(gameObject){}
    public void OnEnter(){
    }
    public void OnExit(){
        _entity.AnimatorExt.SetBool("isAttack", false);
    }
    public override void HandleInput(){
        waitDuration -= Time.deltaTime;
    }
    public override void ProcessGraphics(){
        if( waitDuration < 0.3 ){
            _entity.AnimatorExt.SetBool("isAttack", true);
        }
        _entity.AnimatorExt.SetBool("OnGround", true);
    }
    public override void ProcessPhysics(){
        if( waitDuration < 0 ){
            if( shouldJump ){
                shouldJump = false;
                _entity.Detector.Jump();
            }
            _entity.Detector.Move( );
        }

        if( waitDuration < -0.5f && _entity.Detector.isOnGround() ){
            _stateMachine.ChangeToState(new EnemyIdle(_entity));
        }
    }

}
