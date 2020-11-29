using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKillTriggerHandler : MonoBehaviour, ITriggerHandler
{
    public bool HandleTrigger(Collider2D other)
    {
        StartCoroutine(Restart());
        return true;
    }

    private IEnumerator Restart()
    {
        Player.Instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        LevelManager.Instance.ReloadLevel();
    }
}
