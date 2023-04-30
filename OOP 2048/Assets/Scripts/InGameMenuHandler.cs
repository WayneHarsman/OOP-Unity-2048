using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class InGameMenuHandler : MonoBehaviour
{
    public GameMaster gameMaster;
    public TileBoard inputControl;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private bool isGameOver = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (pauseMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                pauseMenu.SetActive(true);
                inputControl.Pause(true);
                gameMaster.Pause(true);

            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        inputControl.Pause(false);
        gameMaster.Pause(false);
    }

    public void GameOverScreen(int score, double time)
    {
        timeText.text = "Time: " + TimeSpan.FromSeconds(time).ToString("mm\\:ss");
        scoreText.text = "Score: " + score.ToString();
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }
}
