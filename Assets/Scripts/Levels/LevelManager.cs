using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneReference[] _scenes;
    [SerializeField] private int _currentLevelIndex;

    public static LevelManager Instance;

    private Level[] _levels;
    private int _maxLevelIndex;


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

        _levels = new Level[1];
        for (int i = 0; i < _scenes.Length; i++)
        {
            var sceneRef = _scenes[i];
            LoadScene(sceneRef);
        }
    }

    private void LoadScene(SceneReference sceneRef)
    {
        var scene = SceneManager.GetSceneByPath(sceneRef.ScenePath);
        if (scene.isLoaded)
        {
            var a = SceneManager.UnloadSceneAsync(scene);
            a.completed += (ao) =>
            {
                SceneManager.LoadScene(sceneRef.ScenePath, LoadSceneMode.Additive);
            };
        }
        else
        {
            SceneManager.LoadScene(sceneRef.ScenePath, LoadSceneMode.Additive);
        }
    }

    public void RegisterLevel(Level level)
    {
        if (level.levelNumber < 0)
        {
            return;
        }

        if (level.levelNumber >= _levels.Length)
        {
            System.Array.Resize(ref _levels, 2 * _levels.Length);
        }

        _levels[level.levelNumber] = level;

        if (level.levelNumber > _maxLevelIndex)
        {
            _maxLevelIndex = level.levelNumber;
        }

        if (level.levelNumber == _currentLevelIndex)
        {
            level.gameObject.SetActive(false);
        }

        for (int i = 0; i < _maxLevelIndex; i++)
        {
            if (_levels[i] == null)
            {
                return;
            }
        }

        _levels[_currentLevelIndex].Activate();
    }

    public void ReloadLevel()
    {
        Camera_Follow.Instance.SetNewFollowable(Camera_Follow.Instance.transform);
        var level = _levels[_currentLevelIndex];
        var scene = level.gameObject.scene;
        var buildIndex = scene.buildIndex;
        var a = SceneManager.UnloadSceneAsync(scene);
        a.completed += (ao) =>
        {
            SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
        };
    }

    public void NextLevel()
    {
        if (_currentLevelIndex == _maxLevelIndex)
        {
            return;
        }
        _currentLevelIndex++;
        _levels[_currentLevelIndex].Activate();
    }
}
