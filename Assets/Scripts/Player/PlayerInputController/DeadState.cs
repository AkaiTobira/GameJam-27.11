using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState, IState
{
    public DeadState(Entity gameObject) : base(gameObject){}
    public void OnEnter(){
        PlayerAnimator.Instance?.SetTrigger("Dead");
    }
    public void OnExit(){
        PlayerAnimator.Instance?.SetTrigger("Restart");
    }
    public override void HandleInput(){}
    public override void ProcessGraphics(){}
    public override void ProcessPhysics(){}
}
