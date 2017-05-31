using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class ChargeScript : MonoBehaviour {


    public bool isCollected;

	// Use this for initialization
	void Start () {
        isCollected = false;
	}

    // Update is called once per frame
    void Update()
    {
    }

    public void playSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
