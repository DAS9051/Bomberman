//Using the UnityEngine, System.Collections and Scene Management libaries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// PauseMenu class that handels the buttons for the pause menu
public class PauseMenu : MonoBehaviour
{
    // Reference to the pause menu UI GameObject
    public GameObject pauseMenu;
    // Static variable to keep track of whether the game is paused or not
    public static bool ispaused;

    // Use this for initialization
    private void Start()
    {
        // Disable the pause menu on start
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        // Check for Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check if the game is currently paused
            if (ispaused)
            {
                // If it is, call the ResumeGame function
                ResumeGame();
            }
            else
            {
                // If it isn't, call the PauseGame function
                PauseGame();
            }
        }
    }

    // Function to pause the game
    public void PauseGame()
    {
        // Enable the pause menu
        pauseMenu.SetActive(true);
        // Set the time scale to 0, effectively pausing the game
        Time.timeScale = 0f;
        // Set the ispaused variable to true
        ispaused = true;
    }

    // Function to resume the game
    public void ResumeGame()
    {
        // Disable the pause menu
        pauseMenu.SetActive(false);
        // Set the time scale back to 1, effectively resuming the game
        Time.timeScale = 1f;
        // Set the ispaused variable to false
        ispaused = false;
    }

    // Function to go back to main menu
    public void GoToMainMenu()
    {
        // Set the time scale back to 1 and unpause the game
        Time.timeScale = 1f;
        ispaused = false;
        // Load the main menu scene
        SceneManager.LoadScene("Main Menu");
    }
}