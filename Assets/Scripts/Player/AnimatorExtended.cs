using UnityEngine;

public class AnimatorExtended : MonoBehaviour{

    [SerializeField] private Transform _detector;
    [SerializeField] private float _smoothTime;

    [SerializeField] private Animator _animator;

    private Vector3 _animationVel;

    protected virtual void Start() {
    }

    bool EnableFollowing{ get; set; } = true;

    public void UpdateSide( int side){
        if( side == 0 ) return;
        
        Vector3 lScale = transform.localScale;
        lScale.x       = Mathf.Abs( lScale.x) * side;
        transform.localScale = lScale;
    }

    public void SetBool( string name, bool value){
        _animator.SetBool(name, value);
    }

    public void SetFloat( string name, float value){
         _animator.SetFloat(name, value);
    }

    public void SetTrigger( string name){
         _animator.SetTrigger(name);
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