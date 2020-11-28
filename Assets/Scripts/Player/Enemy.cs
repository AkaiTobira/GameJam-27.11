using UnityEngine;

public class Enemy : Entity {

    private FSM AI;

    void Start() {
        AI = new FSM(new EnemyIdle(this));
    }

    void Update() {
        AI.Update();
    }

    void FixedUpdate() {
        AI.FixedUpdate();
    }

}