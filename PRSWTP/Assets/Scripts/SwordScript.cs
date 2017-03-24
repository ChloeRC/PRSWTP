﻿using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour {

    public bool drawn;
    //True for sword-out
    //False for sword-down

    public int damage;

    private float gameTicks;
    private float drawTime;

    private Transform ts;

    public GameObject healthDisplayer;

    // Use this for initialization
    void Start () {
        gameTicks = 0.0f;
        drawTime = 0.3f; //alter this to modify how long the sword stays up
        ts = GetComponent<Transform>();

        if (drawn) {
            swordUp(false);
        }
        else {
            swordDown(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (drawn)
        {
            gameTicks += Time.deltaTime;
        }
	}

	public void toggleSword(bool direction)
    {
        drawn = !drawn;

        if (drawn) { 
			swordUp(direction); 
		}
    }

	public void swordUp (bool direction)
    {
		//sword out facing left
		if (direction) {
			ts.localPosition = new Vector3 (-1, 0.15f, 0);
			Quaternion newRot = Quaternion.Euler (-10, 270, 90);
			ts.localRotation = new Quaternion (newRot.x, newRot.y, newRot.z, newRot.w);
			ts.localScale = new Vector3 (0.055f, 0.1f, 0.1f);
            if (gameTicks > drawTime)
            {
                swordDown(direction);
                gameTicks = 0.0f;
                drawn = !drawn;
            }
		} else {
			//sword out facing right
			ts.localPosition = new Vector3 (1, 0.15f, 0);
			Quaternion newRot = Quaternion.Euler (-10, 90, 90);
			ts.localRotation = new Quaternion (newRot.x, newRot.y, newRot.z, newRot.w);
			ts.localScale = new Vector3 (0.055f, 0.1f, 0.1f);
            if (gameTicks > drawTime)
            {
                swordDown(direction);
                gameTicks = 0.0f;
                drawn = !drawn;
            }
        }
    }

	public void swordDown(bool direction)
    {
        if (direction) {
			ts.localPosition = new Vector3 (0, 0.15f, 0.55f);
			Quaternion newRot = Quaternion.Euler (78, 90, -90);
			ts.localRotation = new Quaternion (newRot.x, newRot.y, newRot.z, newRot.w);
            ts.localScale = new Vector3 (0.09f, 0.1f, 0.08f);
		} else {
			ts.localPosition = new Vector3 (0, 0.15f, -0.55f);
			Quaternion newRot = Quaternion.Euler (78, 90, -90);
			ts.localRotation = new Quaternion (newRot.x, newRot.y, newRot.z, newRot.w);
            ts.localScale = new Vector3(0.04f, 0.1f, 0.08f);
        }
    }

    //this actually controls all the close-up player/enemy collisions because haha
    void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.tag == "Enemy" && drawn)
        {
            Debug.Log("Does anyone have a map?");
            col.gameObject.GetComponent<EnemyAI>().health -= damage;
		}
        //to lose health you also need to be your current self
        else if (col.gameObject.tag == "Enemy" && !drawn  && gameObject.GetComponentInParent<PlayerScript>().getCollisionTime() > 0.5f
            && gameObject.GetComponentInParent<PlayerScript>().getControllable())
        {
            Debug.Log("I'm waving through a window");
            gameObject.GetComponentInParent<PlayerScript>().health--;
            healthDisplayer.GetComponent<HealthBarDisplay>().UpdateText();
            gameObject.GetComponentInParent<PlayerScript>().resetCollisionTime();
        }
    }
}
