using System;
using UnityEngine;

public static class ScoreManager
{
    public static int currentScore { get; private set; }

    public static event Action OnScoreUpdated;

    public static void AddPoint()
    {
        currentScore++;
        OnScoreUpdated?.Invoke();
    }
    public static void ResetScore()
    {
        currentScore = 0;
    }
}