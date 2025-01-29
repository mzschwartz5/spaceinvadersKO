using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static int score;
    private static int lives = 3;
    private static int highscore = 0;
    private static int level;

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
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }

    public static void AddScore(int points)
    {
        score += points;
    }

    public static void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public static void NextLevel()
    {
        level++;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelScene");
    }
}
