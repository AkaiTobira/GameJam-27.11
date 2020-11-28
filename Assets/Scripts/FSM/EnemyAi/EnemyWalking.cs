using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyWalking : BaseState, IState
{

    int direction = Random.Range(0, 2);



    public EnemyWalking(Entity gameObject) : base(gameObject){}
    public void OnEnter(){
        direction = ( direction == 0) ? -1 : 1;
        _entity.AnimatorExt.UpdateSide(direction);
    }
    public void OnExit(){}
    public override void HandleInput(){
    }
    public override void ProcessGraphics(){
        _entity.AnimatorExt.SetBool("OnGround", true);
    }
    public override void ProcessPhysics(){
        
        Debug.Log(_entity.Detector.isNearWall() + " " +  _entity.Detector.isEdgeClose() );

        if( _entity.Detector.isNearWall() || !_entity.Detector.isEdgeClose() ){
            direction *= -1;
            _entity.AnimatorExt.UpdateSide(direction);
        }

        _entity.Detector.Move( direction );
    }

}
