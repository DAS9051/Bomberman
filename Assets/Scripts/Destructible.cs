//Using the UnityEngine and System.Collections libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destructible script for handling destruction of game objects and spawning items on destruction
public class Destructible : MonoBehaviour
{
    // Time in seconds until the game object is destroyed
    public float destructionTime = 1f;

    // Chance (as a value between 0 and 1) that an item will spawn when the game object is destroyed
    [Range(0f, 1f)]
    public float itemSpawnChance = 0.2f;

    // Array of items that can spawn on destruction
    public GameObject[] spawnableItems;

    // Start method runs at the beginning of the object's lifetime
    private void Start()
    {
        // Destroys the object after the specified destruction time
        Destroy(gameObject, destructionTime);
    }

    // OnDestroy method runs when the object is destroyed
    private void OnDestroy()
    {
        // If there are items in the spawnableItems array and a random value is less than the item spawn chance
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance)
        {
            // Choose a random index from the spawnableItems array
            int randomIndex = Random.Range(0, spawnableItems.Length);
            // Spawn the item at the object's position with a default rotation
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }


}
