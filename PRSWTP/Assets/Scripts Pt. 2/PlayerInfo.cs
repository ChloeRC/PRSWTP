using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Using this script to store information about the player across time travel
public class PlayerInfo : MonoBehaviour {
    //for player
	public int health;

	//for health bar display
	public float currWidth;
	public float currPos;

	//for pause function
	public bool pause;

	// Use this for initialization
	void Start () {
        health = 3;	//beginning health value
		currWidth = 0.0f;
		currPos = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setHealth(int i)
    {
        health = i;
    }

    public int getHealth()
    {
        return health;
    }

	public void setPause(bool p)
	{
		pause = p;
        Debug.Log("I'll send you a fully armed battalion to remind you of my love");
	}

	public bool getPause() 
	{
		return pause;
	} 
}
