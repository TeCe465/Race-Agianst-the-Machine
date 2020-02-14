using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingRoomController : MonoBehaviour
{
    //TODO update rayshooter
    public int hitTarget = 0;
    public bool marksmanBool = false;
    public bool athleticBool = false;
    GameObject sc;
    int hh;

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to Scene Controller
        sc = GameObject.Find("Scene Controller");
    }

    // Update is called once per frame
    void Update()
    {
        if(hitTarget == 7)
        {
            marksmanBool = true;
        }

        if(marksmanBool && athleticBool)
        {
            sc.GetComponentInChildren<SceneController>().HelpingHand += 1;
            this.enabled = false;
        }
    }

}
