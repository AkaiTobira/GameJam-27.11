using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MovingPlatform : MonoBehaviour
{
    
    [SerializeField] private List<Vector2> RelativePoints;
    [SerializeField] private float speed;

    private int indexOfNext = 0;
    private Vector2 startingPos;
    
    void Start()
    {
        startingPos = transform.position;
    }

    private Vector2 _movingSpeed;

    public Vector2 MoveSpeed{
        get{ return _movingSpeed;}
    }

    // Update is called once per frame
    void Update()
    {
        if( RelativePoints == null ) return;

        Vector2 currentPonint = RelativePoints[indexOfNext] + startingPos;

        float distnace = (currentPonint - (Vector2)transform.position).magnitude;

        Vector2 direction = (currentPonint - (Vector2)transform.position);
        direction.Normalize();

        if( distnace * Time.deltaTime - speed * Time.deltaTime < 0  ){
            _movingSpeed = direction * distnace * Time.deltaTime;
            transform.Translate( direction * distnace * Time.deltaTime);
            if( distnace < 0.2f) indexOfNext = (indexOfNext + 1)% RelativePoints.Count;
        }else{
            _movingSpeed = direction * speed * Time.deltaTime;
            transform.Translate( direction * speed * Time.deltaTime);
        }

    }

    void OnDrawGizmosSelected() {
                foreach( Vector2 point in RelativePoints){
            DebugDrawHelper.DrawX( (( startingPos == new Vector2() ) ? (Vector2)transform.position : (Vector2)startingPos) + point );
        }
    }

    void OnDrawGizmos() {

    }
}
