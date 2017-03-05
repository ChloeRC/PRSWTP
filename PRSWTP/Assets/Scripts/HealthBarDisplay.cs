﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDisplay : MonoBehaviour {

    public GameObject text;
    public GameObject player;
    private int currHealth;
    private int fullHealth;
    
	// Use this for initialization
	void Start () {
        fullHealth = player.GetComponent<PlayerScript>().health;
        currHealth = fullHealth;

        UpdateHealth();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateHealth()
    {
        currHealth = player.GetComponent<PlayerScript>().health;
        text.GetComponent<TextMesh>().text = "Health: " + currHealth + " / " + fullHealth;
    }
}