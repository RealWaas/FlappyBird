using TMPro;
using UnityEngine;

public class ScoreTemplate : MonoBehaviour
{
    public TMP_Text rank;
    public TMP_Text playerName;
    public TMP_Text highScore;

    public void SetScore(int _rank, ScoreData _scoreData)
    {
        rank.text = _rank.ToString() + ".";
        playerName.text = _scoreData.name;
        highScore.text = _scoreData.highScore.ToString() + " pts";
    }
}
