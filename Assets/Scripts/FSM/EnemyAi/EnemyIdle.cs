using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState
{
    public EnemyIdle(Entity gameObject) : base(gameObject){}
    public void OnEnter(){
    }
    public void OnExit(){}
    public override void HandleInput(){





    }
    public override void ProcessGraphics(){
        _entity.AnimatorExt.SetBool("OnGround", true);
    }
    public override void ProcessPhysics(){}

}
