//Using the UnityEngine, System.Collections and Scene Management libaries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// MainMenu class controls the main menu of the game
public class MainMenu : MonoBehaviour
{
    // playgame method is called when the player clicks on the play button
    // it takes an integer parameter 'players' which indicates the number of players
    public void playgame(int players)
    {
        // set the number of players in the GameManager class
        GameManager.numofplayers = players;
        // load the Bomberman scene
        SceneManager.LoadScene("Bomberman");
    }
}
