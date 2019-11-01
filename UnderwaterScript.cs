using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterScript : MonoBehaviour
{
    AudioSource[] audioSource;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        audioSource = GameObject.Find("RoomAudio").GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        audioSource[0].Play();
        // Turn on the flashlights
        foreach(Light light in player.GetComponentsInChildren<Light>())
        {
            light.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        audioSource[0].Stop();
        // Turn on the flashlights
        foreach(Light light in player.GetComponentsInChildren<Light>())
        {
            light.enabled = false;
        }

    }
}
