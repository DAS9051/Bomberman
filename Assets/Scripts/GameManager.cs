//Using the UnityEngine and System.Collections libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// GameManager script for handeling game settings and number of players
public class GameManager : MonoBehaviour
{
    // Array of player GameObjects
    public GameObject[] players;

    [Header("Powerup SoundEffect")]
    // Audio source for playing sound effects
    public AudioSource source;
    // Clip for powerup pickup sound effect
    public AudioClip pickup;

    // Number of players in the game
    public static int numofplayers = 4;

    // Initialization function
    public void Start()
    {
        // Index of current player
        int index = 1;
        // Iterate through all players
        foreach (GameObject player in players)
        {
            // If the current player index is greater than the number of players in the game
            if (index > numofplayers)
            {
                // Call the Death function on the player's MovementController component
                player.GetComponent<MovementController>().Death();
            }
            // Increment the player index
            index++;
        }
    }

    // Function to check the win state of the game
    public void CheckWinState()
    {
        // Count of alive players
        int aliveCount = 0;
        // Iterate through all players
        foreach (GameObject player in players)
        {
            // If the player is active
            if (player.activeSelf)
            {
                // Increment the alive player count
                aliveCount++;
            }
        }
        // If the number of alive players is less than or equal to 1
        if (aliveCount <= 1)
        {
            // Invoke the NewRound function after 3 seconds
            Invoke(nameof(NewRound), 3f);
        }
    }

    // Function to start a new round
    private void NewRound()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene("Main Menu");
        // Alternatively, reload the current scene:
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Function to play the powerup pickup sound effect
    public void pickupsound()
    {
        source.clip = pickup;
        source.Play();
    }
}