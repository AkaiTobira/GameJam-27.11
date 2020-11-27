using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberEraser : MonoBehaviour
{
    public int erSize;
    public bool Drawing = false;

    private GameObject lastTouched;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == null) return;

            var erasable = hit.collider.GetComponent<Erasable>();
            if (erasable == null) return;

            erasable.UpdateTexture(hit.point);
            Drawing = true;
            lastTouched = hit.collider.gameObject;
        }
        else
        {
            if (lastTouched != null && Drawing)
            {
                Destroy(lastTouched.GetComponent<PolygonCollider2D>());
                lastTouched.AddComponent<PolygonCollider2D>();
            }
            Drawing = false;
           
        }
    }
}
