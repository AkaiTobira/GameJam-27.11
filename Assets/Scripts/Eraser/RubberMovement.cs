using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(target.x, target.y, 0);
    }
}
