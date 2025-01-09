using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(InputManager))]
public class FloppyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    private float jumpForce = 10f;
    private float gracityForce = 4f;

    private bool canJump = false;

    private float rotationSpeed = 5f;

    private float maxUpAngle = 20f;
    private float maxDownAngle = -30f;

    public static event Action OnDeath;

    private void OnEnable()
    {
        InputManager.OnGameStart += LaunchGame;
    }

    private void OnDisable()
    {
        InputManager.OnGameStart -= LaunchGame;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleRotation();
    }

    /// <summary>
    /// Activate controls and gravity to launch the game.
    /// </summary>
    public void LaunchGame()
    {
        canJump = true;

        // Set the gravity of the bird for him to fall
        rb.gravityScale = gracityForce;
    }

    /// <summary>
    /// Handle the jumps of the player
    /// </summary>
    public void HandleJump()
    {
        if (!canJump)
            return;

        // Reset its velocity to have the same jump height
        rb.linearVelocity = Vector2.zero;
        rb.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Constantly Update the rotation of the play to face its velocity on Y
    /// </summary>
    private void HandleRotation()
    {
        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle, maxDownAngle, maxUpAngle);

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!canJump)
            return;

        canJump = false;
        OnDeath?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canJump)
            return;

        ScoreManager.AddPoint();
    }   
}
