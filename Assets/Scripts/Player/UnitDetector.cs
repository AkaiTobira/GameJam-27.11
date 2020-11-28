using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDetector : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private CapsuleCollider2D _cc;

    private Transform _baseParent;
    [SerializeField] private Entity _entity;

    [SerializeField] private WallDetector _wallDetector;

    [SerializeField] private VisionBox _visionBox;



    public bool seePlayer(){
        return (bool)_visionBox?.IsPlayerNear;
    }


    public bool isNearWall(){
        return (bool)_wallDetector?.CheckWall();
    }

    public bool isEdgeClose(){
        return (bool)_wallDetector?.CheckEdge();
    }

    protected virtual void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _cc = GetComponent<CapsuleCollider2D>();

        _baseParent = transform.parent;
        capsuleColliderSize = _cc.size;
    }

    public void Move(){
        CheckGround();
        ApplyMovement();
    }

    public void Move( float direciton ){
        xInput        =(int) direciton;
        Move();
    }

    bool canJump = true;

    public void Jump()
    {
        if (canJump)
        {
            canJump = false;
            isJumping = true;
            _rigidBody.velocity = new Vector2( 0, _entity.JumpForce);
        }
    }   

    public void AddJumpForce()
    {
        _rigidBody.velocity = new Vector2( _rigidBody.velocity.x, _entity.JumpForce);
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
        RaycastHit2D hit = Physics2D.Raycast(bottomCircle.position, Vector2.down, groundCheckRadius, whatIsGround);
        if( hit ) {
            if( hit.collider.tag == "Movable"){
                transform.parent = hit.collider.transform;
            }
        }else{
            transform.parent = _entity.transform;
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
            _rigidBody.velocity = new Vector2(_entity.MovementSpeed * xInput, 0.0f);
        }
        else if (isGrounded && _slopeInfo.isOnSlope && canWalkOnSlope && !isJumping) //If on slope
        {
            _rigidBody.velocity =  new Vector2(_entity.MovementSpeed * _slopeInfo.slopeNormalPerp.x * -xInput, 
                                                _entity.MovementSpeed * _slopeInfo.slopeNormalPerp.y * -xInput);;
        }
        else if (!isGrounded) //If in air
        {
            _rigidBody.velocity = new Vector2(_entity.InAirMovementSpeed * xInput, _rigidBody.velocity.y);
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

    }
}
