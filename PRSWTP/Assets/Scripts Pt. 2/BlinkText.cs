using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkText : MonoBehaviour {

    private bool on;
    private string text;
    private TextMesh tm;
    private float time;

	// Use this for initialization
	void Start () {
        time = 0;
        on = true;
        tm = gameObject.GetComponent<TextMesh>();

        text = tm.text;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= .5)
        {
            on = !on;
            time = 0;

            if (on)
            {
                tm.text = text;
            }
            if (!on)
            {
                tm.text = "";
            }
        }
	}
}
