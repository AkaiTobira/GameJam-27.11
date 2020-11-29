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
        creditsFrame.SetActive(false);
        Cursor.visible = true;
    }
    
    public void StartButton()
    {
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
