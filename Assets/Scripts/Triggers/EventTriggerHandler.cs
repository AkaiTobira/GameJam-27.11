using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerHandler : MonoBehaviour, ITriggerHandler
{
    [SerializeField] private UnityEvent _onTrigger = new UnityEvent();
    public bool HandleTrigger(Collider2D other)
    {
        _onTrigger.Invoke();
        return true;
    }
}
