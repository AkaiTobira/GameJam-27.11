using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugDrawHelper
{


    public static readonly Color DEFAULT_RED = new  Color(1,0,0);
    public const  float X_RAY_LENGHT = 0.1f;

    public static void DrawX( Vector3 targetPosition){

        Debug.DrawLine( 
                targetPosition + new Vector3(  X_RAY_LENGHT,  X_RAY_LENGHT),
                targetPosition + new Vector3( -X_RAY_LENGHT, -X_RAY_LENGHT),
                DEFAULT_RED
            );

            Debug.DrawLine( 
                targetPosition + new Vector3(  X_RAY_LENGHT, -X_RAY_LENGHT),
                targetPosition + new Vector3( -X_RAY_LENGHT, X_RAY_LENGHT),
                DEFAULT_RED
            );
    }


    private static Vector2 Rotate(this Vector2 v, float degrees)
        {
            return Quaternion.Euler(0, 0, degrees) * v;
    }
    

    public static void DrawStar(Vector2 targetPosition, float lenght = X_RAY_LENGHT){

            List<Vector2> directions = new List<Vector2>(){
                 Rotate( new Vector2(lenght/2.0f, lenght/2.0f), 0 ),
                 Rotate( new Vector2(lenght/2.0f, lenght/2.0f), 45 ),
                 Rotate( new Vector2(lenght/2.0f, lenght/2.0f), 90 ),
                 Rotate( new Vector2(lenght/2.0f, lenght/2.0f), 135 ),
                 Rotate( new Vector2(lenght/2.0f, lenght/2.0f), 180 ),
                 Rotate( new Vector2(lenght/2.0f, lenght/2.0f), 225 ),
                 Rotate( new Vector2(lenght/2.0f, lenght/2.0f), 270 ),
                 Rotate( new Vector2(lenght/2.0f, lenght/2.0f), 315 ),                
            };

            for( int i = 0; i < directions.Count; i++){
            Debug.DrawLine( 
                targetPosition,
                targetPosition + directions[i],
                DEFAULT_RED
            );
            }
    }


    public static void DrawX( Vector2 targetPosition){

        Debug.DrawLine( 
                targetPosition + new Vector2(  X_RAY_LENGHT,  X_RAY_LENGHT),
                targetPosition + new Vector2( -X_RAY_LENGHT, -X_RAY_LENGHT),
                DEFAULT_RED
            );

            Debug.DrawLine( 
                targetPosition + new Vector2(  X_RAY_LENGHT, -X_RAY_LENGHT),
                targetPosition + new Vector2( -X_RAY_LENGHT, X_RAY_LENGHT),
                DEFAULT_RED
            );
    }

    public static void DrawX( Vector3 targetPosition, Color c ){

        Debug.DrawLine( 
                targetPosition + new Vector3(  X_RAY_LENGHT,  X_RAY_LENGHT),
                targetPosition + new Vector3( -X_RAY_LENGHT, -X_RAY_LENGHT),
                c
            );

            Debug.DrawLine( 
                targetPosition + new Vector3(  X_RAY_LENGHT, -X_RAY_LENGHT),
                targetPosition + new Vector3( -X_RAY_LENGHT, X_RAY_LENGHT),
                c
            );
    }

    public static void DrawX( Vector2 targetPosition, Color c ){

        Debug.DrawLine( 
                targetPosition + new Vector2(  X_RAY_LENGHT,  X_RAY_LENGHT),
                targetPosition + new Vector2( -X_RAY_LENGHT, -X_RAY_LENGHT),
                c
            );

            Debug.DrawLine( 
                targetPosition + new Vector2(  X_RAY_LENGHT, -X_RAY_LENGHT),
                targetPosition + new Vector2( -X_RAY_LENGHT, X_RAY_LENGHT),
                c
            );
    }

}
