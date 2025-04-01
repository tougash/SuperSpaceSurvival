using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSoundPlayer : MonoBehaviour
{
    public AudioSource laserfireSource;  // Assign in Inspector
    public AudioClip overHeatingClip; // Assign in Inspector
    public AudioClip laserfireClip;   // Assign in Inspector

    public static bool isInMenu = false; // Track menu state
    public static bool isOverheated = false; // Track overheating state

    private bool isFiring = false;

    private bool isOverheating = false;

    void Update()
    {
        
        if (Input.GetMouseButton(0) && !isInMenu && !isOverheated) 
        {
            if (!isFiring) // Prevent multiple calls
            {
                laserfireSource.Stop(); // Stop any previous sound
                isFiring = true;
                laserfireSource.clip = laserfireClip;
                laserfireSource.loop = true;
                laserfireSource.Play(); // Start looping laser sound
            }
        }
        else
        {
            StopFiring(); // Stop firing when released or overheated
        }

        if (isOverheated && !isInMenu) 
        {
            if (!isOverheating) // Prevent multiple calls
            {
                laserfireSource.Stop(); // Stop any previous sound
                isOverheating = true;
                laserfireSource.clip = overHeatingClip;
                laserfireSource.loop = true; // Play while overheated
                laserfireSource.Play(); // Play overheating sound
            }
        }
        else
        {
            StopOverheating(); // Stop firing when released or overheated
        }
    }

    void StopFiring()
    {
        if (isFiring)
        {
            isFiring = false;
            laserfireSource.Stop(); // Immediately stop the sound
        }
    }

    void StopOverheating()
    {
        if (isOverheating)
        {
            isOverheating = false;
            laserfireSource.Stop(); // Immediately stop the sound
        }
    }

    public static void SetMenuState(bool state)
    {
        isInMenu = state;
    }

    public static void SetOverheatedState(bool state)
    {
        isOverheated = state;
    }
}

