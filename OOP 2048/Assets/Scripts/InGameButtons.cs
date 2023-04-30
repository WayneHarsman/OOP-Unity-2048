using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButtons : MonoBehaviour
{
    public GameObject pauseMenu;

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
