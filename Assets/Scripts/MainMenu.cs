using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject HelpMenuPanel;
    //public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        MainPanel.SetActive(true);
        HelpMenuPanel.SetActive(false);
    }

    public void playGame(){
        SceneManager.LoadScene("Radiation");
    }

    public void helpMenu(){
        MainPanel.SetActive(false);
        HelpMenuPanel.SetActive(true);
    }

    //quits the game completely
    public void quit(){
        Application.Quit();
    }

    public void back(){
        MainPanel.SetActive(true);
        HelpMenuPanel.SetActive(false);
    }

    
}
