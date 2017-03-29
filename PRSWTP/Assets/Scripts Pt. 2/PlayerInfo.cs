using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Using this script to store information about the player across time travel
public class PlayerInfo : MonoBehaviour {
    int health;

	// Use this for initialization
	void Start () {
        health = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setHealth(int i)
    {
        //health = i;
        Debug.Log("Do you ever really crash or even make a sound?");
    }

    public int getHealth()
    {
        return health;
    }
}
