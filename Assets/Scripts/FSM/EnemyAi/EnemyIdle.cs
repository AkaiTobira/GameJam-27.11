using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState, IState
{
    float timer = 2.0f;

    public EnemyIdle(Entity gameObject) : base(gameObject){}
    public void OnEnter(){
        timer = 2.0f;
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
