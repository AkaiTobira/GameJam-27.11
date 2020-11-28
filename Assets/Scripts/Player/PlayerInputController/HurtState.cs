using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : BaseState, IState
{
    float timeToEnd = 0.3f;
    public HurtState(Entity gameObject) : base(gameObject){}
    public void OnEnter(){
        timeToEnd = Player.Instance.TimerOfBeeingHit;
        PlayerAnimator.Instance.SetTrigger("GetHit");
    }
    public void OnExit(){}
    public override void HandleInput(){}
    public override void ProcessGraphics(){}
    public override void ProcessPhysics(){
        timeToEnd -= Time.deltaTime;
        if( timeToEnd < 0 ){
            _stateMachine.ChangeToState( new IdleState(_entity));
        }
    }
}
