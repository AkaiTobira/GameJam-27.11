using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneReference[] _levelScenes;
    [SerializeField] private int _currentLevelIndex;

    public static LevelManager Instance;

    private Level[] _levels;

    void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadLevels();
    }

    private void LoadLevels()
    {
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.LoadScene("_Game");
            return;

        }

        _levels = new Level[_levelScenes.Length];
        for (int i = 0; i < _levelScenes.Length; i++)
        {
            var level = _levelScenes[i];
            LoadLevel(level);
        }
    }

    private void LoadLevel(SceneReference level)
    {
        var scene = SceneManager.GetSceneByPath(level.ScenePath);
        if (scene != null && scene.isLoaded)
        {
            var a = SceneManager.UnloadSceneAsync(scene);
            a.completed += (ao) =>
            {
                SceneManager.LoadScene(level.ScenePath, LoadSceneMode.Additive);
            };
        }
        else
        {
            SceneManager.LoadScene(level.ScenePath, LoadSceneMode.Additive);
        }
    }

    public void RegisterLevel(SceneReference scene, Level level)
    {
        var registered = 0;
        for (int i = 0; i < _levels.Length; i++)
        {
            var sceneRef = _levelScenes[i];
            if (sceneRef.ScenePath == scene.ScenePath)
            {
                _levels[i] = level;
                if (i == _currentLevelIndex)
                {
                    level.gameObject.SetActive(false);
                }
            }

            if (_levels[i] != null)
            {
                registered++;
            }
        }

        if (registered == _levels.Length)
        {
            _levels[_currentLevelIndex].Activate();
        }
    }

    public void ReloadLevel()
    {
        Camera_Follow.Instance.SetNewFollowable(Camera_Follow.Instance.transform);
        var levelScene = _levelScenes[_currentLevelIndex];
        var scene = SceneManager.GetSceneByPath(levelScene.ScenePath);
        var a = SceneManager.UnloadSceneAsync(scene);
        a.completed += (ao) =>
        {
            SceneManager.LoadScene(levelScene.ScenePath, LoadSceneMode.Additive);
        };
    }

    public void NextLevel()
    {
        if (_currentLevelIndex == _levels.Length - 1)
        {
            return;
        }
        _currentLevelIndex++;
        _levels[_currentLevelIndex].Activate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }
}
