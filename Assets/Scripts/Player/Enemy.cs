using UnityEngine;

public class Enemy : Entity, IListenToEvents {

    private FSM AI;

    public void OnGameEvent( GameEvent gameEvent){
        switch( gameEvent.type ){
            case GameEventType.PlayerGetsHit:
                AI.ChangeToState(new EnemyIdle(this));
            break;
        }
    }

    void Start() {
        AI = new FSM(new EnemyIdle(this));
        GameEventSystem.RegisterListener( this, GameEventType.PlayerGetsHit);
    }

    void Update() {
        AI.Update();
    }

    void FixedUpdate() {
        AI.FixedUpdate();
    }

}