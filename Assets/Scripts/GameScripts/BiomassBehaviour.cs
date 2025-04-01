using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomassBehaviour : MonoBehaviour
{
    public Camera cam;
    public Transform player;

    public AudioSource expSound;
    int speed = 8;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        expSound = GameObject.FindGameObjectWithTag("Sparkle").GetComponent<AudioSource>();
        transform.LookAt(cam.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerUpgrades playerLevel = other.GetComponent<PlayerUpgrades>();
            playerLevel.currentExp++;
            expSound.PlayOneShot(expSound.clip); // Play the sound effect
            Destroy(gameObject); 
        }
    }
}
