using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private FloppyBehaviour bird;

    private bool isGameStarted = false;

    public static event Action OnGameStart;

    private void Start()
    {
        bird = GetComponent<FloppyBehaviour>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
            bird.HandleJump();
        }
    }

    private void StartGame()
    {
        if (!isGameStarted)
        {
            isGameStarted = true;
            
            ScoreManager.ResetScore();

            OnGameStart?.Invoke();
        }
    }
}
