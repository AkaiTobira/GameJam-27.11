using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public static PlayerDetector Instance;
    private Rigidbody2D _rigidBody;
    private CapsuleCollider2D _cc;

    void Start() {
        if( Instance == null){
            Instance = this;
        }

        _rigidBody = GetComponent<Rigidbody2D>();
        _cc = GetComponent<CapsuleCollider2D>();

        capsuleColliderSize = _cc.size;
    }

    public void Move( float  direciton ){

        xInput        =(int) direciton;
        CheckGround();
        ApplyMovement();
    }

    bool canJump = true;

    public void Jump()
    {
        if (canJump)
        {
            canJump = false;
            isJumping = true;
            _rigidBody.velocity = new Vector2();
            
            _rigidBody.velocity = new Vector2( 0, Player.Instance.JumpForce);
            //_rigidBody.AddForce(new Vector2( 0, Player.Instance.JumpForce), ForceMode2D.Impulse);
        }
    }   

    public void AddJumpForce()
    {
        //Debug.Log("CALLED");
        _rigidBody.velocity = new Vector2( _rigidBody.velocity.x, Player.Instance.JumpForce);
     //   _rigidBody.AddForce(new Vector2( 0, Player.Instance.JumpHoldForce * multipler), ForceMode2D.Force);
    }

    void Update() {
        CheckGround();
    }

    public bool isOnGround(){
        return isGrounded;
    }

    [SerializeField] Transform bottomCircle;
    [SerializeField] float groundCheckRadius;

    public void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(bottomCircle.position, groundCheckRadius, whatIsGround);

        Debug.DrawLine( bottomCircle.position, bottomCircle.position + new Vector3(0, -groundCheckRadius), Color.red );

        if(_rigidBody.velocity.y <= 0.0f)
        {
            isJumping = false;
        }

        if(isGrounded && !isJumping && _slopeInfo.slopeDownAngle <= _slopeInfo.maxSlopeAngle)
        {
            canJump = true;
        }

        CheckForMovingPlatforms();
    }

    private void CheckForMovingPlatforms(){
        RaycastHit2D ssss = Physics2D.Raycast(bottomCircle.position, Vector2.down, groundCheckRadius, whatIsGround);
        if( ssss ) {
            if( ssss.collider.tag == "Movable")
            transform.Translate( ssss.collider.transform.GetComponent<MovingPlatform>().MoveSpeed );
        }
    }


    struct SlopeDetectionInfoPack{
        public Vector2 slopeNormalPerp;
        public float slopeDownAngle;
        public float lastSlopeAngle;
        public float maxSlopeAngle;
        public bool isOnSlope;
        public float slopeSideAngle;
    }

    private float slopeCheckDistance = 5f;
    [SerializeField] private LayerMask whatIsGround;
    bool isGrounded = true;
    bool isJumping = false;
   // Vector2 newVelocity;

    private void ApplyMovement()
    {
        if (isGrounded && !_slopeInfo.isOnSlope && !isJumping) //if not on slope
        {
            _rigidBody.velocity = new Vector2(Player.Instance.MovementSpeed * xInput, 0.0f);
        }
        else if (isGrounded && _slopeInfo.isOnSlope && canWalkOnSlope && !isJumping) //If on slope
        {
            _rigidBody.velocity =  new Vector2(Player.Instance.MovementSpeed * _slopeInfo.slopeNormalPerp.x * -xInput, 
                                                Player.Instance.MovementSpeed * _slopeInfo.slopeNormalPerp.y * -xInput);;
        }
        else if (!isGrounded) //If in air
        {
            _rigidBody.velocity = new Vector2(Player.Instance.InAirMovementSpeed * xInput, _rigidBody.velocity.y);
        }
    }


    private Vector2 capsuleColliderSize = new Vector2();

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, capsuleColliderSize.y / 2));

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront)
        {
            _slopeInfo.isOnSlope = true;
            _slopeInfo.slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            _slopeInfo.isOnSlope = true;
            _slopeInfo.slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            _slopeInfo.slopeSideAngle = 0.0f;
            _slopeInfo.isOnSlope = false;
        }

    }


    private SlopeDetectionInfoPack _slopeInfo;

    bool canWalkOnSlope= true;
    float xInput;
    float fullFriction;
    float noFriction;

    private void SlopeCheckVertical(Vector2 checkPos)
    {      
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);

        if (hit)
        {
            _slopeInfo.slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            _slopeInfo.slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            _slopeInfo.isOnSlope = (_slopeInfo.slopeDownAngle != _slopeInfo.lastSlopeAngle);

            _slopeInfo.lastSlopeAngle = _slopeInfo.slopeDownAngle;
            Debug.DrawRay(hit.point, _slopeInfo.slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        canWalkOnSlope = !( _slopeInfo.slopeDownAngle > _slopeInfo.maxSlopeAngle || 
                            _slopeInfo.slopeSideAngle > _slopeInfo.maxSlopeAngle );

    //    if (isOnSlope && canWalkOnSlope && xInput == 0.0f)
     //   {
     //       rb.sharedMaterial = fullFriction;
    //    }
     //   else
     //   {
     //       rb.sharedMaterial = noFriction;
    //    }
    }
}
