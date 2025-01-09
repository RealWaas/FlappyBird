using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TMP_Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        ScoreManager.OnScoreUpdated += UpdateScore;
    }
    private void OnDisable()
    {
        ScoreManager.OnScoreUpdated -= UpdateScore;
    }

    private void UpdateScore() => scoreText.text = ScoreManager.currentScore.ToString();
}
