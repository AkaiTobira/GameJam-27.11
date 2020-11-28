using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erasable : MonoBehaviour
{
    public Vector2Int lastPos;

    private Texture2D _texture;
    private Color32 _transparent = new Color32(0, 0, 0, 0);
    private Collider2D _myCollider;
    private RubberEraser _rubberEraser;

    // Start is called before the first frame update
    void Start()
    {
        _rubberEraser = FindObjectOfType<RubberEraser>();
        _myCollider = gameObject.GetComponent<Collider2D>();

        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        var originalTexture = spriteRenderer.sprite.texture;
         _texture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, false)
        {
            filterMode = FilterMode.Bilinear,
            wrapMode = TextureWrapMode.Clamp
        };
        var data = originalTexture.GetRawTextureData();
        _texture.LoadRawTextureData(data);
        _texture.Apply();
        spriteRenderer.sprite = Sprite.Create(_texture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f));
    }

    public void UpdateTexture(Vector2 hitPoint, bool resetLastPos = false)
    {
        if (_myCollider == null)
            _myCollider = gameObject.GetComponent<Collider2D>();

        int eraserSize = _rubberEraser.erSize;
        int w = _texture.width;
        int h = _texture.height;
        var mousePos = hitPoint - (Vector2)_myCollider.bounds.min;
        mousePos.x *= w / _myCollider.bounds.size.x;
        mousePos.y *= h / _myCollider.bounds.size.y;
        Vector2Int p = new Vector2Int((int)mousePos.x, (int)mousePos.y);
        Vector2Int start = new Vector2Int();
        Vector2Int end = new Vector2Int();
        if (resetLastPos)
            lastPos = p;
        start.x = Mathf.Clamp(Mathf.Min(p.x, lastPos.x) - eraserSize, 0, w);
        start.y = Mathf.Clamp(Mathf.Min(p.y, lastPos.y) - eraserSize, 0, h);
        end.x = Mathf.Clamp(Mathf.Max(p.x, lastPos.x) + eraserSize, 0, w);
        end.y = Mathf.Clamp(Mathf.Max(p.y, lastPos.y) + eraserSize, 0, h);
        Vector2 dir = p - lastPos;

        var m_Colors = _texture.GetRawTextureData<Color32>();
        for (int x = start.x; x < end.x; x++)
        {
            for (int y = start.y; y < end.y; y++)
            {
                Vector2 pixel = new Vector2(x, y);
                Vector2 linePos = p;
                if (_rubberEraser.drawing)
                {
                    float d = Vector2.Dot(pixel - lastPos, dir) / dir.sqrMagnitude;
                    d = Mathf.Clamp01(d);
                    linePos = Vector2.Lerp(lastPos, p, d);
                }
                if ((pixel - linePos).sqrMagnitude <= eraserSize * eraserSize)
                {
                    m_Colors[x + y * w] = _transparent;
                }
            }
        }
        lastPos = p;
        _texture.Apply();
    }
}
