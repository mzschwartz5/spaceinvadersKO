using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateStateDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreTextComponent;
    private TextMeshProUGUI livesTextComponent;
    private TextMeshProUGUI levelTextComponent;

    void Start()
    {
        scoreTextComponent = transform.Find("Score").GetComponent<TextMeshProUGUI>();
        livesTextComponent = transform.Find("Lives").GetComponent<TextMeshProUGUI>();
        levelTextComponent = transform.Find("Level").GetComponent<TextMeshProUGUI>();

        UpdateScoreDisplay(GameController.score);
        UpdateLivesDisplay(GameController.lives);
        UpdateLevelDisplay(GameController.level);
    }

    void OnEnable()
    {
        GameController.OnScoreChanged += UpdateScoreDisplay;
        GameController.OnLivesChanged += UpdateLivesDisplay;
        GameController.OnLevelChanged += UpdateLevelDisplay;
    }

    void OnDisable()
    {
        GameController.OnScoreChanged -= UpdateScoreDisplay;
        GameController.OnLivesChanged -= UpdateLivesDisplay;
        GameController.OnLevelChanged -= UpdateLevelDisplay;
    }

    void UpdateScoreDisplay(int newScore)
    {
        scoreTextComponent.text = "Score: " + newScore;
    }

    void UpdateLivesDisplay(int newLives)
    {
        livesTextComponent.text = "Lives: " + newLives;
    }
    void UpdateLevelDisplay(int newLevel)
    {
        levelTextComponent.text = "Level: " + newLevel;
    }
}
