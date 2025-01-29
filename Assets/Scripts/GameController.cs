using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static int score;
    public static int lives = 3;
    public static int highscore = 0;
    public static int level;

    public delegate void ScoreChangedHandler(int newScore);
    public static event ScoreChangedHandler OnScoreChanged;

    public delegate void LivesChangedHandler(int newLives);
    public static event LivesChangedHandler OnLivesChanged;

    public delegate void LevelChangedHandler(int newLevel);
    public static event LevelChangedHandler OnLevelChanged;

    public delegate void HighScoreChangedHandler(int newHighScore);
    public static event HighScoreChangedHandler OnHighScoreChanged;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Keep this object alive between scenes
    }

    public static void Quit()
    {
        Application.Quit();

    }

    public static void StartGame()
    {
        score = 0;
        lives = 3;
        level = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelScene");
    }

    public static void GameOver()
    {
        if (score > highscore)
        {
            highscore = score;
            OnHighScoreChanged?.Invoke(highscore);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }

    public static void AddScore(int points)
    {
        score += points;
        OnScoreChanged?.Invoke(score);
    }

    public static void LoseLife()
    {
        lives--;
        OnLivesChanged?.Invoke(lives);
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public static void NextLevel()
    {
        level++;
        OnLevelChanged?.Invoke(level);
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelScene");
    }
}
