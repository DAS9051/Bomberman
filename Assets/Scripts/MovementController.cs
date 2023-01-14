using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set;}
    private Vector2 direction = Vector2.down;
    public float speed = 5f;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public AnimatedSpriteRender spriteRenderUp;
    public AnimatedSpriteRender spriteRenderDown;
    public AnimatedSpriteRender spriteRenderLeft;
    public AnimatedSpriteRender spriteRenderRight;
    public AnimatedSpriteRender spriteRendererdDeath;
    private AnimatedSpriteRender activeSpriteRenderer;



    private void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRenderDown;
    }

    private void Update()
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRenderUp);
        } else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteRenderDown);
        } else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteRenderLeft);
        } else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRenderRight);
        } else
            SetDirection(Vector2.zero, activeSpriteRenderer);
        {

        }
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            Death();
        }
    }

    private void Death()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRenderUp.enabled = false;
        spriteRenderDown.enabled = false;
        spriteRenderLeft.enabled = false;
        spriteRenderRight.enabled = false;
        spriteRendererdDeath.enabled = true;

        Invoke(nameof(OnDeathEnded), 1.25f);
    }

    private void OnDeathEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }
}

