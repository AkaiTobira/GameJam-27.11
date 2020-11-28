using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Transform _detector;
    [SerializeField] private float _smoothTime;

    public static PlayerAnimator Instance;
    public static Animator AnimatorInstance;
    private Vector3 _animationVel;

    void Start() {
        if( Instance == null){
            Instance = this;
            AnimatorInstance = GetComponent<Animator>();
        }
    }

    bool EnableFollowing{ get; set; } = true;

    public void UpdateSide( int side){
        if( side == 0 ) return;
        
        Vector3 lScale = transform.localScale;
        lScale.x       = Mathf.Abs( lScale.x) * side;
        transform.localScale = lScale;
    }

    void Update()
    {
        if( EnableFollowing ){
            transform.position = Vector3.SmoothDamp( transform.position, 
                                                    _detector.position, 
                                                    ref _animationVel, 
                                                    _smoothTime);
        }
    }
}
