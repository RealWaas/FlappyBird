using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject scoreText;
    public GameObject startWindow;
    public GameObject gameOverWindow;
    public GameObject leaderBoardWindow;

    public GameObject scoreDisplayerPrefab;

    private void OnEnable()
    {
        InputManager.OnGameStart += OnGameStart;
        FloppyBehaviour.OnDeath += OnGameEnd;
    }

    private void OnDisable()
    {
        InputManager.OnGameStart -= OnGameStart;
        FloppyBehaviour.OnDeath -= OnGameEnd;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startWindow.SetActive(true);
        gameOverWindow.SetActive(false);
        leaderBoardWindow.SetActive(false);

        scoreText.SetActive(false);
    }

    public void OnGameStart()
    {
        startWindow.SetActive(false);
        gameOverWindow.SetActive(false);
        leaderBoardWindow.SetActive(false);

        scoreText.SetActive(true);
    }

    public void OnGameEnd()
    {
        startWindow.SetActive(false);
        gameOverWindow.SetActive(true);
        leaderBoardWindow.SetActive(false);

        scoreText.SetActive(true);
    }

    public void OnShowBoard()
    {
        startWindow.SetActive(false);
        gameOverWindow.SetActive(false);
        leaderBoardWindow.SetActive(true);

        scoreText.SetActive(false);
    }

    /// <summary>
    /// Reload the scene for the game to restart.
    /// </summary>
    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
