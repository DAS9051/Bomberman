using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    public static int numofplayers = 4;
    
    public void Start(){
        int index = 1;
        foreach (GameObject player in players){
            if (index>numofplayers){
                player.GetComponent<MovementController>().Death();
            }
            index++;

        }
    }
    public void CheckWinState()
    {
        int aliveCount = 0;
        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                aliveCount++;
            }
        }
    if (aliveCount <= 1){
        Invoke(nameof(NewRound), 3f);
    }
    }
    private void NewRound()
    {
        SceneManager.LoadScene("Main Menu");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


