using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  public GameObject GamePausePanel;
  public GameObject menuKeyCanvas;
  public GameObject helpMenu;
  public bool isPaused;
  public GameManager gameManager;

  // Start is called before the first frame update
  void Start()
  {
    GamePausePanel.SetActive(false);
    helpMenu.SetActive(false);
    isPaused = false;
    Console.WriteLine("PAUSE SETUP");
  }

  // Update is called once per frame
  void Update()
  {
    Console.WriteLine("UPDATE");
    if (Input.GetKeyDown(KeyCode.Escape)){
      if(isPaused){
        resume();
      }
      else
      {
        pause();
      }
    }
  }

  //Resumes the game
  public void resume(){
    GamePausePanel.SetActive(false);
    helpMenu.SetActive(false);
    Time.timeScale = 1f;
    gameManager.unpause();
    isPaused = false;
  }

  //Pauses the game and opens menu
  public void pause(){
    GamePausePanel.SetActive(true);
    Time.timeScale = 0f;
    gameManager.pauseGame();
    isPaused = true;
  }

  public void help(){
    helpMenu.SetActive(true);
    GamePausePanel.SetActive(false);
  }

  public void helpMenuBack(){
    helpMenu.SetActive(false);
    GamePausePanel.SetActive(true);
  }
    

  //sends player to main menu
  public void mainMenu(){
    Time.timeScale = 1f;
    SceneManager.LoadScene("MainMenu");
  }

  //quits the game completely
  public void quit(){
    Application.Quit();
  }
}
