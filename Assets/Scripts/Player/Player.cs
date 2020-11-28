using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IListenToEvents
{
    public static Player Instance;
    private FSM playerController;

    [SerializeField] private float InvincibleTime = 1.0f;
    [SerializeField] private float timeOfbeingHit = 0.3f;

    public float TimerOfBeeingHit{
        get { return timeOfbeingHit;}
    }

    float reactionToGetHitTimer = 1.0f;

    bool isDead;

    public void OnGameEvent( GameEvent gameEvent){
        if( isDead ) return;

        switch( gameEvent.type ){
            case GameEventType.PlayerGetsHit:
                if( reactionToGetHitTimer < 0){
                    playerController.ChangeToState(new HurtState(this));
                    reactionToGetHitTimer = InvincibleTime;
                    GameEventSystem.RiseEvent( new GameEvent(GameEventType.LoseHp));
                }
            break;
        }
    }

    void Start() {
        if( Instance == null ){
            Instance = this;
        }

        GameEventSystem.RegisterListener( this, GameEventType.PlayerGetsHit);
        playerController = new FSM(new IdleState(this));
    }

    void Update() {
        playerController.Update();
        reactionToGetHitTimer -= Time.deltaTime;
        PlayerJumpCounter.Update();
    }

    void FixedUpdate() {
        playerController.FixedUpdate();
    }
}
