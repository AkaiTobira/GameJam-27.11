using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    [SerializeField] Transform frontCircle;
    [SerializeField] float WallCheckRadius;

    [SerializeField] float FloorCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    private bool isNearWall = false;
    private bool isEdgeClear = false;

    void Update() {
        isNearWall  = Physics2D.OverlapCircle(frontCircle.position, WallCheckRadius, whatIsGround);
        isEdgeClear = Physics2D.Raycast( frontCircle.position , Vector2.down, FloorCheckRadius );

        DebugDrawHelper.DrawStar(frontCircle.position, WallCheckRadius);
        Debug.DrawLine(frontCircle.position, frontCircle.position + Vector3.down * FloorCheckRadius, Color.green );
    }

    public bool CheckEdge(){
        return isEdgeClear;
    }

    public bool CheckWall()
    {
        return isNearWall;
    }



}
