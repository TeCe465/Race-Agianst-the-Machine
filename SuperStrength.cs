using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperStrength : MonoBehaviour
{
    PlayerCharacter player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        player.maxCarryWeight = 30;
    }
}
