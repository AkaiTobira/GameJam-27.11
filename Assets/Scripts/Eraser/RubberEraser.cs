using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RubberEraser : MonoBehaviour
{
    public int erSize;
    public int sizeFactor;
    public bool drawing = false;
    public int updateTextureEachFrame;

    private List<GameObject> _lastTouched = new List<GameObject>();
    private List<Collider2D> _nowTouched;
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
            var otherColliders = Physics2D.OverlapCircleAll(mousePos, erSize / sizeFactor, LayerMask.GetMask("Floor"));
            //var otherColliders = Physics2D.OverlapCircleAll(mousePos, erSize / sizeFactor);
            if (otherColliders.Length == 0)
            {
                RegenerateColliders();
                return;
            };

            _nowTouched = otherColliders.ToList();
            foreach (var otherCollider in otherColliders)
            {
                var erasable = otherCollider.GetComponent<Erasable>();
                if (erasable == null)
                {
                    RegenerateColliders();
                    return;
                }

                if (_frameCount == 0)
                {
                    var otherGameObject = otherCollider.gameObject;

                    erasable.UpdateTexture(mousePos, !drawing || !_lastTouched.Contains(otherGameObject));

                    drawing = true;

                    RegenerateColliders();

                    if(!_lastTouched.Contains(otherGameObject))_lastTouched.Add(otherGameObject);
                }
            }
        }
        else
        {
            _nowTouched = new List<Collider2D>();
            if (drawing)
            {
                RegenerateColliders();
            }
            drawing = false;
        }
    }

    private void RegenerateColliders()
    {
        if (_lastTouched.Count == 0) return;
        var temp = new List<GameObject>();
        foreach (var item in _lastTouched)
        {
            var isRegenerated = RegenerateCollider(item);
            if (!isRegenerated) temp.Add(item);
        }
        if (_nowTouched.Count == 0)
        {
            _lastTouched = new List<GameObject>();
        }
        else
        {
            _lastTouched = temp;
        }
    }

    bool RegenerateCollider(GameObject gameObject)
    {
        //var gameObject = collider.gameObject;
        if (_nowTouched.Contains(gameObject.GetComponent<PolygonCollider2D>())) return false;
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        return true;
    }
}
