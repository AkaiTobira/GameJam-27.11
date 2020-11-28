using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : UnitDetector
{
    public static PlayerDetector Instance;

    protected override void Start() {
        if( Instance == null){
            Instance = this;
        }

        base.Start();
    }
}
