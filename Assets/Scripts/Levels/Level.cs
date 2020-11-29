using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelNumber;

    [SerializeField] private Transform _playerSpawn;
    [SerializeField][Range(-1, 1)] private int _playerFacing;
    [SerializeField] private Camera _cameraPlaceholder;

    void Start()
    {
        _cameraPlaceholder.gameObject.SetActive(false);
        if (LevelManager.Instance)
        {
            LevelManager.Instance.RegisterLevel(this);
        }
    }

    public void Activate()
    {
        if (_playerSpawn)
        {
            PlayerAnimator.Instance.UpdateSide(_playerFacing);
            PlayerAnimator.Instance.transform.position = _playerSpawn.position;
            PlayerDetector.Instance.transform.position = _playerSpawn.position;
        }

        if (_cameraPlaceholder)
        {
            Camera_Follow.Instance.SetNewFollowable(_cameraPlaceholder.transform);
            Camera_Follow.Instance.SetZoom(_cameraPlaceholder.orthographicSize);
        }

        gameObject.SetActive(true);
    }
}
