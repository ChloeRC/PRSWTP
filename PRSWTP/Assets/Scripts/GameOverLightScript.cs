using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLightScript : MonoBehaviour {

	private float minIntensity = 0f;
    private float maxIntensity = 0.4f;
    private float timer;
    private float currIntensity;
    private Light l;
    private bool increment;

    // Use this for initialization
    void Start()
    {
        timer = 0f;
        currIntensity = 0f;
        l = GetComponent<Light>();
        l.intensity = minIntensity;
        increment = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!increment && timer > 1)
        {
            increment = true;
        }

        if (increment && timer > .1 && l.intensity < maxIntensity)
        {
            l.intensity = currIntensity;
            currIntensity += .01f;
            timer = 0f;
        }
    }
}
