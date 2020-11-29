using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public interface ITriggerHandler
{
    bool HandleTrigger(Collider2D other);
}

[RequireComponent(typeof(BoxCollider2D))]
public class Trigger : MonoBehaviour
{
    [SerializeField] private Color _color = new Color(1f, 0.9f, 0.3f, 0.4f);
    [SerializeField] private bool _enabled = true;
    [SerializeField] private bool _oneShot;

    private Color _disabledColor;
    private ITriggerHandler[] _handlers;

    private void OnEnable()
    {
        _handlers = GetComponents<ITriggerHandler>();
        _disabledColor = new Color(_color.r, _color.g, _color.b, 0.05f);
        _color = new Color(_color.r, _color.g, _color.b, 0.4f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_enabled)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            foreach (var handler in _handlers)
            {
                handler.HandleTrigger(other);
            }
            if (_oneShot)
            {
                _enabled = false;
                // Destroy(gameObject);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var collider = GetComponent<BoxCollider2D>();
        Gizmos.color = _enabled ? _color : _disabledColor;
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawCube(collider.offset, collider.size);

        Gizmos.color = Color.white;
        Gizmos.matrix = Matrix4x4.identity;
    }
#endif
}
