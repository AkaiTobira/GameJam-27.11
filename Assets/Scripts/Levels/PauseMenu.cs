using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject frame;

    private void Awake()
    {
        HidePauseMenu();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseFrame();
        }
    }

    public void HidePauseMenu()
    {
        frame.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void ShowPauseMenu()
    {
        frame.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadSceneAsync("_MainMenu");
    }

    private void TogglePauseFrame()
    {
        if (frame.activeSelf) HidePauseMenu();
        else ShowPauseMenu();
    }
}
