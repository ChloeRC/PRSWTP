using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDisplay : MonoBehaviour {

    public GameObject text;
    public GameObject player;
    private int health;
    private int full;
    
	// Use this for initialization
	void Start () {
        health = player.GetComponent<PlayerScript>().health;
        full = player.GetComponent<TrackMovement>().currLevelCharges();

        text.GetComponent<TextMesh>().text = "Health: " + health + " / " + full;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateHealth()
    {
        text.GetComponent<TextMesh>().text = "Health: " + health + " / " + full;
    }
}
