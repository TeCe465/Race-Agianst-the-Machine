using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;
    float Multiplier = 3f;

    void Start()
    {
        Player = GameObject.Find("Player");
        Player.GetComponent<FPSInput>().defaultSpeedMultiplier = Multiplier; 
    }
}
