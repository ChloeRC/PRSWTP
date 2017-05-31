using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeScript : MonoBehaviour {

    public bool isCollected;

	// Use this for initialization
	void Start () {
        isCollected = false;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<AudioSource>().Play();

    }
}
