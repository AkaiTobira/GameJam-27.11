using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private SceneReference _scene;
    [SerializeField] private Transform _playerSpawn;
    [SerializeField] private Camera _cameraPlaceholder;

    void Start()
    {
        if (LevelManager.Instance)
        {
            _cameraPlaceholder.enabled = false;
            LevelManager.Instance.RegisterLevel(_scene, this);
        }
    }

    public void Activate()
    {
        PlayerAnimator.Instance.UpdateSide(1);
        PlayerAnimator.Instance.transform.position = _playerSpawn.position;
        PlayerDetector.Instance.transform.position = _playerSpawn.position;
        Camera.main.GetComponent<Camera_Follow>().SetNewFollowable(_cameraPlaceholder.transform);
        Camera.main.GetComponent<Camera_Follow>().SetZoom(_cameraPlaceholder.orthographicSize);
    }
}
