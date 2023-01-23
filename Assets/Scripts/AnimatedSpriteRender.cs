//Using the UnityEngine and System.Collections libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The main class that handles the animated sprite rendering
public class AnimatedSpriteRender : MonoBehaviour
{
    //A reference to the sprite renderer component of the object
    private SpriteRenderer spriteRenderer;

    //The idle sprite to be used when the animation is not playing
    public Sprite idleSprite;
    //An array of sprites to be used for the animation
    public Sprite[] animationSprites;

    //The time between animation frames
    public float animationTime = 0.25f;
    //The current frame of the animation
    private int animationFrame;

    //Should the animation loop or stop at the last frame
    public bool loop = true;
    //Should the animation start in idle mode or playing
    public bool idle = true;

    //Initialize the sprite renderer reference on awake
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Enables the sprite renderer when the object is enabled
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    //Disables the sprite renderer when the object is disabled
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    //Starts the animation by invoking the nextFrame function repeatedly
    private void Start()
    {
        InvokeRepeating(nameof(nextFrame), animationTime, animationTime);
    }

    //Advances the animation to the next frame
    private void nextFrame()
    {
        animationFrame++;

        //If looping is enabled and the animation has reached the end, go back to the first frame
        if (loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        //If in idle mode, display the idle sprite
        if (idle)
        {
            spriteRenderer.sprite = idleSprite;
        }
        //Otherwise, display the current frame of the animation
        else if (animationFrame >= 0 && animationFrame < animationSprites.Length)
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }

}
