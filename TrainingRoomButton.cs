using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingRoomButton : MonoBehaviour, Interactable
{
    GameObject trc; //TrainingRoomController

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to Training Room Controller
        trc = GameObject.Find("TrainingRoomController");

        //Set color of button to red
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.red;
    }

    public void Interact()
    {
        //Set color of button to green
        Renderer render = GetComponent<Renderer>();
        render.material.color = Color.green;

        trc.GetComponentInChildren<TrainingRoomController>().athleticBool = true;
    }
}
