//Using the UnityEngine and System.Collections libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MovementController class that handels the movement for the player
public class MovementController : MonoBehaviour
{
    // Declare a variable to store the Rigidbody2D component
    public new Rigidbody2D rigidbody { get; private set; }
    // Declare a variable to store the direction of movement
    private Vector2 direction = Vector2.down;
    // Declare a variable to store the speed of movement
    public float speed = 5f;

    // Declare variables for keybinds
    [Header("Keybinds")]
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    // Declare variables for sprite renderers for different animations
    [Header("Animations")]
    public AnimatedSpriteRender spriteRenderUp;
    public AnimatedSpriteRender spriteRenderDown;
    public AnimatedSpriteRender spriteRenderLeft;
    public AnimatedSpriteRender spriteRenderRight;
    public AnimatedSpriteRender spriteRendererdDeath;
    private AnimatedSpriteRender activeSpriteRenderer;

    // Declare variables for sound effects
    [Header("Sound Effects")]
    public AudioSource source;
    public AudioClip deathsound;

    // Get the Rigidbody2D component on Awake
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRenderDown;
    }

    // Check for input and update direction and animation on Update
    private void Update()
    {
        if (!PauseMenu.ispaused)
        {
            if (Input.GetKey(inputUp))
            {
                SetDirection(Vector2.up, spriteRenderUp);
            }
            else if (Input.GetKey(inputDown))
            {
                SetDirection(Vector2.down, spriteRenderDown);
            }
            else if (Input.GetKey(inputLeft))
            {
                SetDirection(Vector2.left, spriteRenderLeft);
            }
            else if (Input.GetKey(inputRight))
            {
                SetDirection(Vector2.right, spriteRenderRight);
            }
            else
                SetDirection(Vector2.zero, activeSpriteRenderer);


        }
    }

    // Move the rigidbody based on direction and speed on FixedUpdate
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    // Set the direction and animation based on input
    private void SetDirection(Vector2 newDirection, AnimatedSpriteRender spriteRender)
    {
        direction = newDirection;

        spriteRenderUp.enabled = spriteRender == spriteRenderUp;
        spriteRenderDown.enabled = spriteRender == spriteRenderDown;
        spriteRenderLeft.enabled = spriteRender == spriteRenderLeft;
        spriteRenderRight.enabled = spriteRender == spriteRenderRight;

        activeSpriteRenderer = spriteRender;
        activeSpriteRenderer.idle = direction == Vector2.zero;

    }
    // Check for collision with explosion and trigger death sequence
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            source.clip = deathsound;
            source.Play();
            Death();
        }
    }

    // Disable movement and activate death animation and sound
    public void Death()
    {
        // Disable movement and bomb dropping
        enabled = false;
        GetComponent<BombController>().enabled = false;

        // Disable all movement animations
        spriteRenderUp.enabled = false;
        spriteRenderDown.enabled = false;
        spriteRenderLeft.enabled = false;
        spriteRenderRight.enabled = false;

        // Enable death animation
        spriteRendererdDeath.enabled = true;

        // Invoke a method to deactivate game object and check for win state after a delay
        Invoke(nameof(OnDeathEnded), 1.25f);
    }

    // Deactivate game object and check for win state
    private void OnDeathEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }

}

