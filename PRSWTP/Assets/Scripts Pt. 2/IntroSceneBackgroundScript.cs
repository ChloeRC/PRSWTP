using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneBackgroundScript : MonoBehaviour {

    public Material noName;
    public Material withName;

    private float timer;
    private bool isNameVisible;

    private Renderer rend;

	// Use this for initialization
	void Start () {
        isNameVisible = true;
        timer = 0f;
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        //if name is visible, it lasts for 1 second
        //if the name isn't visible, it lasts for half a second
        if ((isNameVisible && timer > 1f) || (!isNameVisible && timer > .5f))
        {
            timer = 0f;
            isNameVisible = !isNameVisible;
        }

        if (isNameVisible)
        {
            rend.material = withName;
        }
        else
        {
            rend.material = noName;
        }
	}
}
