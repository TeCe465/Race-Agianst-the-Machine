using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject sc; //SceneController
    int hh; //HelpingHand

    void Start()
    {
        //Get reference to Scene Controller
        sc = GameObject.Find("Scene Controller");

        //Get Helping Hand component from Scene Controller
        hh = sc.GetComponentInChildren<SceneController>().HelpingHand;

        Debug.Log("Starting Helping Hand: " + hh);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter()
    {
        //Destroy(other.gameObject);

        hh += 1;
        Debug.Log("Added Helping Hand: " + hh);

        //Destroy(other.BoxCollider);
    }
}
