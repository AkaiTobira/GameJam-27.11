using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTriggerHandler : MonoBehaviour, ITriggerHandler
{
    public bool HandleTrigger(Collider2D other)
    {
        LevelManager.Instance.NextLevel();
        return true;
    }
}
