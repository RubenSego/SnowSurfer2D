using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float boostSpeed = 30f;
    [SerializeField] private ParticleSystem snowEffect;
    [SerializeField] private ScoreManager scoreManager; //Reference to the ScoreManager script to update the score.

    SurfaceEffector2D se;
    Rigidbody2D rb;
    
    float baseSpeed;
    InputAction moveAction;
    Vector2 moveInput;
    float previousRotation; // Store the previous rotation of the player.
    float totalRotation; // Store the total rotation of the player.
    int flipCount; // Store the number of flips the player has performed.

    private bool canControlPlayer = true; // Flag to control player input.
    public bool CanControlPlayer { get => canControlPlayer; set => canControlPlayer = value; }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        se = FindAnyObjectByType<SurfaceEffector2D>();
        baseSpeed = se.speed;
    }

    void Update()
    {
        if(!canControlPlayer) return; // If player input is disabled, exit the method.
        
        PlayerTorque();
        BoostPlayer();
        CalculateFlips();        
    }
    /// <summary>
    /// Calculates the number of flips the player has made based on their rotation.
    /// </summary>
    private void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z; //Get the current rotation of the player in degrees.
        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation); // Calculate the change in rotation since the last frame and add it to the total rotation.
        if (Math.Abs(totalRotation) > 340)
        {
            flipCount++;
            scoreManager.AddScore(flipCount*100); // Update the score in the ScoreManager script.
            totalRotation = 0; // Reset the total rotation after a flip is counted.
        }
        previousRotation = currentRotation; // Update the previous rotation for the next frame.
    }

    /// <summary>
    /// Applies torque to the player based on input from the Move action.
    /// </summary>
    void PlayerTorque()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.x < 0)
        {
            rb.AddTorque(torqueAmount);
        }
        else if (moveInput.x > 0)
        {
            rb.AddTorque(-torqueAmount);
        }
    }
    void BoostPlayer()
    {
        // Increase the player' speed when the up arrow is pressed.
        // Surface Effector speed is increased to boostSpeed.
        if (moveInput.y > 0)
        {
            se.speed = boostSpeed;
        }
        else
        {
            se.speed = baseSpeed; // Return to regular speed.
        }
    }
    // Detects when the player collides with the floor and plays the boost effect.
    void OnCollisionEnter2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if(collision.gameObject.layer == layerIndex)
        {
            snowEffect.Play();
        }
    }
    // Detects when the player collides with the floor and plays the boost effect.
    void OnCollisionExit2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if(collision.gameObject.layer == layerIndex)
        {
            snowEffect.Stop();
        }
    }
}
