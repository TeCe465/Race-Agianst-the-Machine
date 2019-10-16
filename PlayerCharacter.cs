using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public bool isAlive = true;
    public bool OnFloor = false;
    public bool takingDamage = false;
    public bool holdingObj = false;

    void Start()
    {
        health = maxHealth;
    }

    //need a way to make sure health doesnt go over 100
    private void Update()
    {
        if (health > maxHealth)
            health = maxHealth;

        if (health <= 0)
        {
            isAlive = false;
            health = 0;
        }
        else
            isAlive = true;
    }

    public void Hurt(int damage)
    {
        if (!takingDamage)
            StartCoroutine(TakeDamage(damage));
    }

    void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.collider.name);
        OnFloor = collision.collider.name == "Floor";
    }


    IEnumerator TakeDamage(int damage)
    {
        takingDamage = true;

        if (health <= damage)
            health = 0;
        else
            health -= damage;

        yield return new WaitForSeconds(1f);
        takingDamage = false;

    }


}
