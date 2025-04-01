using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstepsSource;
    public float footstepDelay = 0.5f; // Delay between footsteps

     private bool isWalking = false;

    // Update is called once per frame
    void Update()
    {
         {
        bool pressingKeys = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;

        if (pressingKeys && !isWalking)
        {
            isWalking = true;
            StartCoroutine(PlayFootsteps()); // Start playing footsteps
        }
        else if (!pressingKeys && isWalking)
        {
            isWalking = false;
            footstepsSource.Stop(); // Stop playing footsteps
        }
    }
    }


    IEnumerator PlayFootsteps()
    {
        while (isWalking)
        {
            footstepsSource.Play(); // Play footstep sound
            yield return new WaitForSeconds(footstepDelay); // Wait before playing next step
        }
    }
}
