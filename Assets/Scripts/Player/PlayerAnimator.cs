using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : AnimatorExtended
{
    public static PlayerAnimator Instance;
    public static Animator AnimatorInstance;

    protected override void Start() {
        if( Instance == null){
            Instance = this;
            
        }
        AnimatorInstance = GetComponent<Animator>();
        base.Start();
    }
}
