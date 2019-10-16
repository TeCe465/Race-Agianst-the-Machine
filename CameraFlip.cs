using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : MonoBehaviour
{
    // Start is called before the first frame update
    public bool flipped = false;
    float timeLeft = 10f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!flipped)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                float flipTime = Random.Range(3, 7);
                StartCoroutine(flip(flipTime));
            }
        }
        // wait for flipping to be finished

    }

    IEnumerator flip(float flipTime)
    {
        flipped = true;

        yield return new WaitForSeconds(flipTime);
        flipped = false;
        timeLeft = Random.Range(5, 10);
    }
}
