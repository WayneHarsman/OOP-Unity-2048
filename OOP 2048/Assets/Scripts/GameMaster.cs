using System;
using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public TileBoard board;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI timeText;

    public GameObject uiController;

    public int Score => score;

    public event Action OnScoreChanged;
    public event Action OnGameStarted;
    public event Action OnGameOver;

    private int score;
    private double comboTime;
    private double time;
    private int hiscore;
    private bool isPaused = false;

    private void Start()
    {
        NewGame();
        OnScoreChanged += () => scoreText.text = score.ToString();
        OnGameStarted += () => Debug.Log("Game started");
        OnGameOver += () => uiController.GetComponent<InGameMenuHandler>().GameOverScreen(score, time);
        hiscore = LoadHiscore();
        InvokeRepeating("setTime", 0, 1.0f);
    }
    private void Update()
    {
        if (!isPaused)
            comboTime += 1f * Time.deltaTime;
    }

    public void Pause(bool _pause) { isPaused = _pause; }
    public void NewGame()
    {
        // reset score
        SetScore(0);

        // update board state
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;

        OnGameStarted?.Invoke();
    }

    public void GameOver()
    {
        board.enabled = false;
        CancelInvoke();
        OnGameOver?.Invoke();

    }


    public void IncreaseTime()
    {
        if (!isPaused)
            time += 1;
    }


    public void setTime()
    {
        IncreaseTime();
        timeText.text = TimeSpan.FromSeconds(time).ToString("mm\\:ss");
    }
    public void IncreaseScore(int pPoints)
    {
        if (comboTime < 0.5f) comboTime = 0.5f;

        pPoints = (int)(pPoints * (0.2f / comboTime));
        SetScore(score + pPoints);
        if (score > hiscore) SetHiscore();
        comboTime = 0f;
    }


    private void SetScore(int pScore)
    {
        this.score = pScore;
        OnScoreChanged?.Invoke();

        SaveHiscore();
    }

    private void SetHiscore()
    {
        scoreLabel.text = "NEW HIGH SCORE!";
    }

    private void SaveHiscore()
    {
        int hiscore = LoadHiscore();

        if (score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    private int LoadHiscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }

}
