
using UnityEngine;

public static class PlayerInput{

    public static bool isLeftHold(){
        return Input.GetAxis("Horizontal") == -1;
    }

    public static bool isRightHold(){
        return Input.GetAxis("Horizontal") == 1;
    }

    public static bool isJumpPressed(){
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);
    }

}