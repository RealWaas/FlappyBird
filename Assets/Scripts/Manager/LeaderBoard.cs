using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public static class LeaderBoard
{
    const int LEADER_BOARD_SIZE = 10;

    public static List<ScoreData> ScoreBoard { get; private set; } = new List<ScoreData>();

    public static void AddScore(ScoreData _score)
    {
        int previousIndex = ScoreBoard.FindIndex((element) => element.name == _score.name);

        if (previousIndex != -1) // Player already existing
        {
            ScoreData previousScore = ScoreBoard[previousIndex];

            // Update score if beaten
            if(_score.highScore >= previousScore.highScore) 
                ScoreBoard[previousIndex] = _score;
        }
        else // Player doesn't exist yet
        {
            // Add score if enought room for it
            if(ScoreBoard.Count < LEADER_BOARD_SIZE)
            {
                ScoreBoard.Add(new ScoreData {  name = _score.name, highScore = _score.highScore });
            }
            else if (_score.highScore >= ScoreBoard[LEADER_BOARD_SIZE-1].highScore)
            {
                ScoreBoard.RemoveAt(LEADER_BOARD_SIZE - 1);
                ScoreBoard.Add(new ScoreData { name = _score.name, highScore = _score.highScore });
            }
        }

        ScoreBoard = OrderScoreBoard();
        SaveScoreBoard();
    }

    private static List<ScoreData> OrderScoreBoard()
    {
        return ScoreBoard.OrderByDescending(element => element.highScore).ToList();
    }

    #region data managment
    [RuntimeInitializeOnLoadMethod]
    private static void LoadScoreBoard()
    {
        string json = PlayerPrefs.GetString("leaderboard", "[]");
        ScoreBoard = JsonConvert.DeserializeObject<ScoreData[]>(json).ToList();
    }

    private static void SaveScoreBoard()
    {
        string json = JsonConvert.SerializeObject(ScoreBoard);
        PlayerPrefs.SetString("leaderboard", json);
        PlayerPrefs.Save();
    }
    #endregion
}

public struct ScoreData
{
    public string name;
    public int highScore;
}
