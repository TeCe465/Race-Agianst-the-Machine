using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerCam : MonoBehaviour
{
    // Start is called before the first frame update
    Camera playerCam;
    void Start()
    {
        //Debug.Log("Enabled");
        playerCam = GameObject.Find("MainCamera").GetComponent<Camera>();
        transform.position = playerCam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerCam.transform.position);
    }
}
