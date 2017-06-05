using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameLightScript : MonoBehaviour {

    private float minIntensity = 0f;
    private float maxIntensity = 0.4f;
    public float timer;
    private float currIntensity;
    private Light l;
    private bool isFading;
    private bool inTransition;

    public GameObject rend;
    public Material credits;
    public GameObject risingText;
    public GameObject mainMenuText;

    // Use this for initialization
    void Start()
    {
        timer = 0f;
        currIntensity = 0f;
        l = GetComponent<Light>();
        l.intensity = minIntensity;
        inTransition = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        rend.GetComponent<Renderer>().material = credits;
        if (timer > 1f)
        {
            inTransition = false;
            risingText.GetComponent<RisingText>().isRising = true;
        }

        if (timer > .1f && l.intensity < maxIntensity && !inTransition)
        {
            l.intensity = currIntensity;
            currIntensity += .01f;
            timer = 0f;
        }

        if (timer > 12f)
        {
            mainMenuText.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
