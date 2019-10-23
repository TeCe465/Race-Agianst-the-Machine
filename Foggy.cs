using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foggy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0.6f;

    }

    private void OnDisable()
    {
        RenderSettings.fog = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
