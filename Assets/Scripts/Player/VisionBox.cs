using UnityEngine;

class VisionBox: MonoBehaviour {
    

    private bool _isPlayerNear;

    public bool IsPlayerNear{
        get { return _isPlayerNear;}
    }


    void OnTriggerEnter2D(Collider2D other) {
        if( other?.tag == "Player"){
            _isPlayerNear = true;
        }
    }


    void OnTriggerExit2D(Collider2D other) {
        if( other?.tag == "Player"){
            _isPlayerNear = false;
        }
    }
}