using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject menu;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseBehaviour.instance.isPaused)
            {
                menu.SetActive(false);
                PauseBehaviour.instance.unpauseGame();
            }
            else
            {
                menu.SetActive(true);
                PauseBehaviour.instance.pauseGame();
            }
        }
    }

    public void exitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
