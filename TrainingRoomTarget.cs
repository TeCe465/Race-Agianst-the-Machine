using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingRoomTarget : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject trc; //TrainingRoomController
    int hitTarget;

    void Start()
    {
        //Get reference to Training Room Controller
        trc = GameObject.Find("TrainingRoomController");
    }

    private void OnTriggerEnter()
    {
        trc.GetComponentInChildren<TrainingRoomController>().hitTarget += 1;

        Destroy(this);
    }
}
