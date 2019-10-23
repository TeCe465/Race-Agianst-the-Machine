using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperStrength : MonoBehaviour
{
    PlayerCharacter player;
    private float defaultCarryWeight;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        defaultCarryWeight = player.maxCarryWeight;
        player.maxCarryWeight = 30f;
    }
    private void OnDisable()
    {
        player.maxCarryWeight = defaultCarryWeight;
    }
}
