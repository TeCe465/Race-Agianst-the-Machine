using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterScript : MonoBehaviour
{
    AudioSource[] audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioController").GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        audioSource[0].Play();
        // Turn on the flashlights
        foreach(Light light in GameObject.Find("Player").GetComponentsInChildren<Light>())
        {
            light.enabled = true;
        }
    }
}
