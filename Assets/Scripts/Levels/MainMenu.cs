using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    public GameObject creditsFrame;
    public Button start;

    void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        creditsFrame.SetActive(false);
    }
    
    public void StartButton()
    {
       // LoadManager.instance.LoadScene(("_Game")
        SceneManager.LoadSceneAsync("_Game");
        start.interactable = false;
    }
    public void CreditsButton()
    {
        creditsFrame.SetActive(true);
    }
    public void CreditsBackButton()
    {
        creditsFrame.SetActive(false);
    }
    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
