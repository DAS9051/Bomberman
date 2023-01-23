//Using the UnityEngine and System.Collections libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//The main class that handles the placing and exploding of the bombs
public class BombController : MonoBehaviour
{

    [Header("Bomb")]
    public GameObject bombPrefab; // The prefab for the bomb object
    public KeyCode inputKey = KeyCode.Space; // The input key for placing a bomb
    public float bombFuseTime = 3f; // The time before the bomb explodes
    public int bombAmount = 1; // The number of bombs the player starts with
    private int bombsRemaining; // The number of bombs the player currently has left

    [Header("Explosion")]
    public Explosion explosionPrefab; // The prefab for the explosion object
    public LayerMask explosionLayerMask; // The layer mask for objects that can be destroyed by explosions
    public float explosionDuration = 1f; // The duration of the explosion animation
    public int explosionRadius = 1; // The radius of the explosion

    [Header("Sound Effects")]
    public AudioSource source; // The audio source for playing sound effects
    public AudioClip place, explode; // The sound effect for placing and exploding bombs

    [Header("Destructible")]
    public Tilemap destructibleTiles; // The tilemap for destructible tiles
    public Destructible destructiblePrefab; // The prefab for the destructible object

    // When the script is enabled, set the number of bombs remaining to the starting number of bombs
    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }

    // In the update loop, check if the player has pressed the input key and if there are bombs remaining
    private void Update()
    {
        if (!PauseMenu.ispaused)
        {
            if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
            {
                StartCoroutine(PlaceBomb()); // Calls the function asynchronous
            }
        }
    }

    // Coroutine for placing a bomb
    private IEnumerator PlaceBomb()
    {
        // Play the place sound effect
        source.clip = place;
        source.Play();

        // Get the player's current position and round it to the nearest integer
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        // Instantiate the bomb prefab at the player's position
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;

        // Wait for the bomb fuse time
        yield return new WaitForSeconds(bombFuseTime);

        // Play the explode sound effect
        source.clip = explode;
        source.Play();

        // Get the bomb's position and round it to the nearest integer
        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        // Instantiate the explosion prefab at the bomb's position
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start); // Set the starting sprite for the explosion
        explosion.DestroyAfter(explosionDuration); // Destroy the explosion after the duration

        // Call the explode function in all 4 directions (up, down, left, right) with the current position and explosion radius
        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        // Destroy the bomb gameobject
        Destroy(bomb);
        // Increase the number of bombs remaining
        bombsRemaining++;
    }
    // Function for handling the explosion
    private void Explode(Vector2 position, Vector2 direction, int Length)
    {
        // If the length is less than or equal to 0, exit the function
        if (Length <= 0)
        {
            return;
        }

        // Increase the position by the direction
        position += direction;

        // Check if there is an object in the explosion layer mask at the current position
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            // If there is, clear the destructible object at that position
            ClearDestructible(position);
            return;
        }

        // Instantiate the explosion prefab at the current position
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        // Set the active renderer based on whether the current length is greater than 1 (middle) or not (end)
        explosion.SetActiveRenderer(Length > 1 ? explosion.middle : explosion.end);
        // Set the direction of the explosion
        explosion.SetDirection(direction);
        // Destroy the explosion after the explosion duration
        explosion.DestroyAfter(explosionDuration);

        // Recursively call the explode function with the updated position, direction and length
        Explode(position, direction, Length - 1);
    }

    // When the player exits a collider, check if it is a bomb and set it to not be a trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }

    // Clear the destructible object at a position
    private void ClearDestructible(Vector2 position)
    {
        // Get the cell position of the destructible object
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);

        // If there is a tile at that position
        if (tile != null)
        {
            // Instantiate the destructible prefab at that position
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            // Remove the tile from the tilemap
            destructibleTiles.SetTile(cell, null);
        }
    }

    // Function for adding a bomb to the player's inventory
    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }

}
