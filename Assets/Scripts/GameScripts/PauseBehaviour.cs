using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour
{
    public static PauseBehaviour instance;
    public bool isPaused = false;

    private void Awake()
    {
        if(instance == null) instance = this;
    }

    public void pauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void unpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }
}
