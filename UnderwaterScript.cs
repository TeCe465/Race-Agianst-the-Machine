using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterScript : MonoBehaviour
{
    AudioSource[] audioSource;
    Light[] lightSource;

    void Start()
    {
        lightSource = GameObject.Find("Player").GetComponentsInChildren<Light>();
        audioSource = GameObject.Find("RoomAudio").GetComponents<AudioSource>();
    }

    //void Update()
    //{
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Head")
        {
            audioSource[0].Play();
            // Turn on the flashlights
            foreach (Light light in lightSource)
            {
                light.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Head")
        {
            audioSource[0].Stop();
            // Turn off the flashlights
            foreach (Light light in lightSource)
            {
                light.enabled = false;
            }
        }
    }
}
