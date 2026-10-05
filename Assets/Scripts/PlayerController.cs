using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float boostSpeed = 30f;
    [SerializeField] private ParticleSystem snowEffect;
    InputAction moveAction;
    Vector2 moveInput;
    Rigidbody2D rb;
    SurfaceEffector2D se;
    float baseSpeed;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        se = FindAnyObjectByType<SurfaceEffector2D>();
        baseSpeed = se.speed;
    }

    void Update()
    {
        PlayerTorque();
        BoostPlayer();
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
