using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour {

    private Color transparent;
    private Color opaque;
    private float timer;
    private float currOpacity;
    private TextMesh tm;
    private bool increment;

    // Use this for initialization
    void Start()
    {
        transparent = new Color(1, 1, 1, 0);
        opaque = new Color(1, 1, 1, 1);
        timer = 0f;
        currOpacity = 0f;
        tm = GetComponent<TextMesh>();
        tm.color = transparent;
        increment = false;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (!increment && timer > 2)
        {
            increment = true;
        }

        if (increment && timer > .1 && tm.color != opaque)
        {
            tm.color = new Color(1, 1, 1, currOpacity);
            currOpacity += .03f;
            timer = 0f;
        }
	}
}
