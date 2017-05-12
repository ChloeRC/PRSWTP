using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script is intended to represent all the past selves
 * in order to differentiate current/past selves from each
 * other. More betterer.
 * 
 * It might not work. Sorry :(
 * It definitely doesn't work.s
*/

public class CloneScript : MonoBehaviour {
    private float gameTicks;
    public bool isDead;

    // Use this for initialization
    void Start () {
        isDead = false;

	}
	
	// Update is called once per frame
	void Update () {
    }

    public void setRotation()
    {
        //insert math here that calculates differences
        //in x values from setPosition :)
    }

    //taken from PlayerScript - sets position of the clone
    public void setLocation(CloneLocation cloneLocation)
    {
        if (!cloneLocation.Equals(TrackMovement.DESTROY_CLONE))
        {
            transform.position = cloneLocation.getLocation();
            transform.rotation = Quaternion.Euler(cloneLocation.getRotation());
            
            if (cloneLocation.getDidSword())
            {
                Debug.Log("STABBY STAB STAB");
            }
            else if (cloneLocation.getDidShoot())
            {
                Debug.Log("One two three four five six seven--");
            }
        }
        else
        {
            this.kill();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerScript>().kill();
            Debug.Log("You colllided with your past self!!");
        }
    }

    public void kill()
    {
        Destroy(this.gameObject);
    }
}
