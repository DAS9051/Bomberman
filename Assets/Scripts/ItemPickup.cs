//Using the UnityEngine and System.Collections libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Itempickup script for picking up items
public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }

    // The type of item that this pickup will grant the player
    public ItemType type;

    // Called when the player picks up this item
    private void OnItemPickup(GameObject player)
    {
        // Play the pickup sound
        FindObjectOfType<GameManager>().pickupsound();

        // Grant the player the item based on the type of this pickup
        switch (type)
        {
            case ItemType.ExtraBomb:
                // Add an extra bomb to the player's inventory
                player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.BlastRadius:
                // Increase the player's bomb blast radius
                player.GetComponent<BombController>().explosionRadius++;
                break;
            case ItemType.SpeedIncrease:
                // Increase the player's movement speed
                player.GetComponent<MovementController>().speed++;
                break;
        }

        // Destroy this pickup object
        Destroy(gameObject);
    }

    // Detect when the player enters the trigger area of this pickup
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger area is the player
        if (other.CompareTag("Player"))
        {
            // Call the OnItemPickup method and pass in the player object
            OnItemPickup(other.gameObject);
        }
    }
}
