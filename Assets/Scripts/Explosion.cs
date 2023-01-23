//Using the UnityEngine and System.Collections libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Explotion script for handling the sprite for the explosion
public class Explosion : MonoBehaviour
{
    // Public variables to hold the different renderers for the start, middle, and end of the explosion animation
    public AnimatedSpriteRender start;
    public AnimatedSpriteRender middle;
    public AnimatedSpriteRender end;

    // Method to set which renderer is active
    public void SetActiveRenderer(AnimatedSpriteRender render)
    {
        // Enabling the chosen renderer and disabling the others
        start.enabled = render == start;
        middle.enabled = render == middle;
        end.enabled = render == end;
    }

    // Method to set the direction of the explosion
    public void SetDirection(Vector2 direction)
    {
        // Calculating the angle of the explosion using Atan2 and setting the rotation of the transform
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    // Method to destroy the game object after a certain amount of seconds
    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
