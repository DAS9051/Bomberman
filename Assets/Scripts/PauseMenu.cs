using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public static bool ispaused;
    private void Start(){
        pauseMenu.SetActive(false);
    }

    private void Update(){

        if (Input.GetKeyDown(KeyCode.Escape)){
            if(ispaused){
                ResumeGame();
            }
            else{
                PauseGame();
            }
        }

    }

    public void PauseGame(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        ispaused = true;
    }

    public void ResumeGame(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        ispaused = false;
    }

    public void GoToMainMenu(){
        Time.timeScale = 1f;
        ispaused = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame(){
        Application.Quit()
    }
}
