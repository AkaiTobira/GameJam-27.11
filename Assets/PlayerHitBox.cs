using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if( (bool)other?.tag.Contains("Enemy")){
            GameEventSystem.RiseEvent( new GameEvent( GameEventType.PlayerGetsHit));
        }
    }
}
