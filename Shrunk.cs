using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrunk : MonoBehaviour
{
    PlayerCharacter player;
    public float PercentShrink = 75;
    void Start()
    {
        
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        //player.transform.localScale.Set(0.5f, 0.5f, 0.5f);
        player.transform.localScale -= new Vector3(PercentShrink/100,PercentShrink/100 ,PercentShrink/100);

    }
}
