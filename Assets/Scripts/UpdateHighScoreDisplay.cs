using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateHighScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI highScoreTextComponent;

    void Start()
    {
        highScoreTextComponent = transform.Find("Highscore").GetComponent<TextMeshProUGUI>();
        UpdateHighScoreDisplayText(GameController.highscore);
    }

    void OnEnable()
    {
        GameController.OnHighScoreChanged += UpdateHighScoreDisplayText;
    }

    void OnDisable()
    {
        GameController.OnHighScoreChanged -= UpdateHighScoreDisplayText;
    }

    void UpdateHighScoreDisplayText(int newHighScore)
    {
        highScoreTextComponent.text = "Highscore: " + newHighScore;
    }
}
