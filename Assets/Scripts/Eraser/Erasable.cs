using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class Erasable : MonoBehaviour
{
    public Vector2Int lastPos;

    private Texture2D _texture;
    private Sprite _sprite;
    private Color32 _transparent = new Color32(0, 0, 0, 0);
    private Collider2D _myCollider;
    private int pixelsLeft;
    private NativeArray<Color32> _colors;

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = gameObject.GetComponent<Collider2D>();

        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        var originalTexture = spriteRenderer.sprite.texture;
        Debug.Log(originalTexture.format);
         _texture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, false)
        {
            filterMode = FilterMode.Bilinear,
            wrapMode = TextureWrapMode.Clamp
        };
        var data = originalTexture.GetPixels();
        _texture.SetPixels(data);
        _texture.Apply();
        _sprite = Sprite.Create(_texture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f));
        spriteRenderer.sprite = _sprite;
        _colors = _texture.GetRawTextureData<Color32>();

        pixelsLeft = _colors.Count(c => c.a != 0);
    }

    public void UpdateTexture(Vector2 hitPoint, bool resetLastPos = false)
    {
        if (_myCollider == null)
        {
            _myCollider = gameObject.GetComponent<Collider2D>();
        }

        int eraserSize = RubberEraser.Instance.erSize;
        int w = _texture.width;
        int h = _texture.height;
        var mousePos = TextureSpaceCoord(hitPoint);
        Vector2Int p = new Vector2Int((int)mousePos.x, (int)mousePos.y);
        Vector2Int start = new Vector2Int();
        Vector2Int end = new Vector2Int();
        if (resetLastPos)
        {
            lastPos = p;
        }
        start.x = Mathf.Clamp(Mathf.Min(p.x, lastPos.x) - eraserSize, 0, w);
        start.y = Mathf.Clamp(Mathf.Min(p.y, lastPos.y) - eraserSize, 0, h);
        end.x = Mathf.Clamp(Mathf.Max(p.x, lastPos.x) + eraserSize, 0, w);
        end.y = Mathf.Clamp(Mathf.Max(p.y, lastPos.y) + eraserSize, 0, h);
        Vector2 dir = p - lastPos;

        for (int x = start.x; x < end.x; x++)
        {
            for (int y = start.y; y < end.y; y++)
            {
                Vector2 pixel = new Vector2(x, y);
                Vector2 linePos = p;
                if (RubberEraser.Instance.drawing)
                {
                    float d = Vector2.Dot(pixel - lastPos, dir) / dir.sqrMagnitude;
                    d = Mathf.Clamp01(d);
                    linePos = Vector2.Lerp(lastPos, p, d);
                }
                if ((pixel - linePos).sqrMagnitude <= eraserSize * eraserSize)
                {
                    if (_colors[x + y * w].a != 0)
                    {
                        pixelsLeft--;
                        _colors[x + y * w] = _transparent;
                    }
                }
            }
        }
        lastPos = p;
        _texture.Apply();

        if (pixelsLeft <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 0.5f);
        }
    }

    // https://toqoz.svbtle.com/finding-sprite-uv-texture-coordinates-in-unity
    public Vector2 TextureSpaceCoord(Vector3 worldPos)
    {
        float ppu = _sprite.pixelsPerUnit;

        // Local position on the sprite in pixels.
        Vector2 localPos = transform.InverseTransformPoint(worldPos) * ppu;

        // When the sprite is part of an atlas, the rect defines its offset on the texture.
        // When the sprite is not part of an atlas, the rect is the same as the texture (x = 0, y = 0, width = tex.width, ...)
        var texSpacePivot = new Vector2(_sprite.rect.x, _sprite.rect.y) + _sprite.pivot;
        Vector2 texSpaceCoord = texSpacePivot + localPos;

        return texSpaceCoord;
    }
}
