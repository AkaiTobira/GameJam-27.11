
using UnityEngine;

public static class PlayerInput{

    public static bool isLeftHold(){
        return Input.GetAxisRaw("Horizontal") == -1;
    }

    public static bool isRightHold(){
        return Input.GetAxisRaw("Horizontal") == 1;
    }

    public static bool isJumpPressed(){
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);
    }

    public static bool isJumpHold(){
        return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W);
    }

}