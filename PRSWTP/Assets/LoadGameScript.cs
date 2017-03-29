using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameScript : MonoBehaviour {

    private Color transparent;
    private Color opaque;
    private float timer;
    private float currOpacity;

    // Use this for initialization
    void Start()
    {
        transparent = new Color(1, 1, 1, 0);
        opaque = new Color(1, 1, 1, 1);
        timer = 0f;
        currOpacity = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > .1)
        {
            //GetComponent<Text>().color = new Color(1, 1, 1, currOpacity);
            currOpacity += .1f;
            timer = 0f;
        }
	}
}
