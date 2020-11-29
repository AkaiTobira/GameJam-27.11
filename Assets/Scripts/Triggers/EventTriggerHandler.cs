using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActivateObjectDef
{
    public GameObject gameObject;
    public bool enabled;
}

public class ActivateObjectsTriggerHandler : MonoBehaviour, ITriggerHandler
{
    [SerializeField] private ActivateObjectDef[] _activateObjects;
    public bool HandleTrigger(Collider2D other)
    {
        foreach (var def in _activateObjects)
        {
            def.gameObject.SetActive(def.enabled);
        }
        return true;
    }
}
