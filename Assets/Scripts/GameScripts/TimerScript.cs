using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text timerText; // Timer UI text (Use Text, not GameObject)
    private int counter = 0; // Counter variable
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("IncrementCounter", 1f, 1f); // Calls method every 1 second
    }

    // Update is called once per frame
    void IncrementCounter()
    {
        counter++; // Increment counter
        timerText.text = counter.ToString(); // Update timer text
    }
}
