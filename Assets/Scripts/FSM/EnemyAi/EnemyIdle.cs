using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState, IState
{
    float timer = 1.2f;

    public EnemyIdle(Entity gameObject) : base(gameObject){}
    public void OnEnter(){
        timer = 1.2f;
        _entity.AnimatorExt.SetBool("isMoving", false);
    }
    public void OnExit(){}
    public override void HandleInput(){
        timer -= Time.deltaTime;
        if( timer < 0 ){
            _stateMachine.ChangeToState( new EnemyWalking(_entity));
        }
    }
    public override void ProcessGraphics(){
        _entity.AnimatorExt.SetBool("OnGround", true);
    }
    public override void ProcessPhysics(){}

}
