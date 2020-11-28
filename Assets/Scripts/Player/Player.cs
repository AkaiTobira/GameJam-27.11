using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public static Player Instance;
    private FSM playerController;

    void Start() {
        if( Instance == null ){
            Instance = this;
        }

        playerController = new FSM(new IdleState(this));
    }

    void Update() {
        playerController.Update();
    }

    void FixedUpdate() {
        playerController.FixedUpdate();
    }
}
