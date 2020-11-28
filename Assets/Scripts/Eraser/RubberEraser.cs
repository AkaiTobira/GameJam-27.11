using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberEraser : MonoBehaviour
{
    public int erSize;
    public bool drawing = false;
    public int updateTextureEachFrame;

    private Collider2D _lastTouched;
    private int _frameCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _frameCount = ++_frameCount % updateTextureEachFrame;
        if (Input.GetMouseButton(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var otherCollider = Physics2D.OverlapCircle(mousePos, erSize / 100);
            if (otherCollider == null)
            {
                if (_lastTouched != null)
                {
                    RegenerateCollider(_lastTouched);
                    _lastTouched = null;
                }
                return;
            };

            var erasable = otherCollider.GetComponent<Erasable>();
            if (erasable == null)
            {
                if (_lastTouched != null)
                {
                    RegenerateCollider(_lastTouched);
                    _lastTouched = null;
                }
                return;
            }

            if (_frameCount == 0)
            {
                erasable.UpdateTexture(mousePos, !drawing || _lastTouched != otherCollider);

                drawing = true;

                if (_lastTouched != null && _lastTouched != otherCollider)
                {
                    RegenerateCollider(_lastTouched);
                }

                _lastTouched = otherCollider;
            }
        }
        else
        {
            if (_lastTouched != null && drawing)
            {
                RegenerateCollider(_lastTouched);
                _lastTouched = null;
            }
            drawing = false;

        }
    }

    void RegenerateCollider(Collider2D collider)
    {
        var gameObject = collider.gameObject;
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }
}
