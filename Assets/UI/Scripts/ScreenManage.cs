using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManage : MonoBehaviour
{
    
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void SetFulScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
