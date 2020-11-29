using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKillTriggerHandler : MonoBehaviour, ITriggerHandler
{
    public bool HandleTrigger(Collider2D other)
    {
        LevelManager.Instance.ReloadLevel();
        return true;
    }
}
