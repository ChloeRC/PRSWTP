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

	public void setCurrWidth(float f)
	{
		if (currWidth == 0.0f) 
		{
			currWidth = f;
			Debug.Log ("info currWidth: " + currWidth);
		}
	}

	public float getCurrWidth()
	{
		Debug.Log ("Hey guys!!!");
		return currWidth;
	}

	public void setCurrPos(float f)
	{
		if (currPos == 0.0f) 
		{
			currPos = f;
			Debug.Log ("info currPos: " + currPos);
		}
	}

	public float getCurrPos() {
		Debug.Log ("falafel");
		return currPos;
	}
}
