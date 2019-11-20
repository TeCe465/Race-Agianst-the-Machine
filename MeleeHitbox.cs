using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    //this makes enemy's melee attacks hurt me
    NPC enemy;
    public int damage = 20;

    private void Start()
    {
        enemy = GetComponentInParent<NPC>();
        Debug.Log(enemy.name);
    }

    void OnTriggerEnter(Collider other)
    {
        if (enemy.isAlive)
        {
            if (other.name == "Player")
            {
                PlayerCharacter player = other.GetComponent<PlayerCharacter>();
                player.Hurt(damage);
            }
        }
    }
}
