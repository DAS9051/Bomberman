using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void playgame(int players){
        GameManager.numofplayers = players;
        SceneManager.LoadScene("Bomberman");
    }
    public void quitgame(){
        Application.ExternalCall("CloseTab");
    }
}
