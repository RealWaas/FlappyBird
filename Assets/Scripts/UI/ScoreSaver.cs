using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSaver : MonoBehaviour
{
    private int finalScore;
    public TMP_Text scoreText;
    public TMP_Text playerNameInput;
    public Button validateButton;

    public void AddScore()
    {
        if (string.IsNullOrWhiteSpace(playerNameInput.text))
            return;

        LeaderBoard.AddScore(new ScoreData { name = playerNameInput.text, highScore = ScoreManager.currentScore });
        validateButton.interactable = false;
    }
}
