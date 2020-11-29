using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioClipTriggerHandler : MonoBehaviour, ITriggerHandler
{
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public bool HandleTrigger(Collider2D other)
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        return true;
    }
}
