using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(PauseBehaviour.instance.isPaused)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PauseBehaviour.instance.unpauseGame();
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
