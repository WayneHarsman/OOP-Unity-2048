using System;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public TileBoard board;

    public int Score => score;

    public event Action OnScoreChanged;
    public event Action OnGameStarted;
    public event Action OnGameOver;

    private int score;

    private void Start()
    {
        NewGame();
        OnScoreChanged += () => Debug.Log($"Score: {score}");
        OnGameStarted += () => Debug.Log("Game started");
        OnGameOver += () => Debug.Log("Game over");
    }

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

        OnGameOver?.Invoke();
    }

    public void IncreaseScore(int pPoints)
    {
        SetScore(score + pPoints);
    }

    private void SetScore(int pScore)
    {
        this.score = pScore;
        OnScoreChanged?.Invoke();

        SaveHiscore();
    }

    private void SaveHiscore()
    {
        int hiscore = LoadHiscore();

        if (score > hiscore) {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    private int LoadHiscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }

}
