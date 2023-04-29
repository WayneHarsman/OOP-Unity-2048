using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    public GameObject mainMenuCanvas;
    public GameObject preGameCanvas;
    public GameObject optionsCanvas;


    public void OpenPreGameMenu()
    {
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        preGameCanvas.SetActive(true);
    }

    public void OpenMainMenu()
    {
        optionsCanvas.SetActive(false);
        preGameCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void OpenOptionsMenu()
    {
        preGameCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
