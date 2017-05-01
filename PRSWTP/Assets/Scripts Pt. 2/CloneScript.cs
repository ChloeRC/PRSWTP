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
    GameObject clone;
    private float gameTicks;
    public bool isDead;
    private Vector3 cloneLoc;

    // Use this for initialization
    void Start () {
        isDead = false;

	}
	
	// Update is called once per frame
	void Update () {
        getPosition();
	}

    public void setRotation()
    {
        //insert math here that calculates differences
        //in x values from setPosition :)
    }

    //taken from PlayerScript - sets position of the clone
    public void setPosition(Vector3 vector)
    {
        cloneLoc = vector;
        Debug.Log("Goal Position: " + cloneLoc);
        CloneScript clone = GetComponent<CloneScript>();
        var marker = TrackMovement.MARKER;
        if (marker != null && Mathf.Abs((vector - marker).magnitude) < float.Epsilon)
        {
            Debug.Log("oh no!");
            kill();
        }

        transform.position = vector;
        Debug.Log("Actual Position: " + transform.position);
        //transform.position = vector;
        //Debug.Log("Clone's position: " + transform.position);
    }

    void getPosition()
    {
        transform.position = cloneLoc;
        Debug.Log("Clone position: " + transform.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //PlayerScript.GetComponent<PlayerScript>().health--;
            Debug.Log("You colllided with your past self!!");
            Debug.Log("the man loved trees");
        }
    }

    void kill()
    {
        Debug.Log("k");
        //Destroy(this.gameObject);
    }

}
