
using UnityEngine;

public class Entity : MonoBehaviour {

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpHoldTime;
    [SerializeField] private float _inAirMovementSpeed;

    public float InAirMovementSpeed{
        get { return _inAirMovementSpeed;}
    }

    public float JumpHoldTime{
        get { return _jumpHoldTime;}
    }
    public float JumpForce{
        get { return _jumpForce;}
    }

    public float MovementSpeed{
        get { return _movementSpeed;}
    }

}