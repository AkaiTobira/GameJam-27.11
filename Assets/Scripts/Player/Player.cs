using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private FSM playerController;

    [SerializeField] private float _movementSpeed;
    [SerializeField] public  float _jumpForce;


    public float JumpForce{
        get { return _jumpForce;}
    }

    public float MovementSpeed{
        get { return _movementSpeed;}
    }

    void Start() {
        if( Instance == null ){
            Instance = this;
        }

        playerController = new FSM(new IdleState(gameObject));
    }

    void Update() {
        playerController.Update();
    }

    void FixedUpdate() {
        playerController.FixedUpdate();
    }
}
