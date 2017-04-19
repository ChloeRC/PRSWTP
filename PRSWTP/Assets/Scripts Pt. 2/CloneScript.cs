using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script is intended to represent all the past selves
 * in order to differentiate current/past selves from each
 * other. 
 * 
 * It might not work. Sorry :(
 * 
*/

public class CloneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //taken from PlayerScript
    public void setPosition(Vector3 vector)
    {
        var marker = TrackMovement.MARKER;
        //this is always true?
        if (marker != null && Mathf.Abs((vector - marker).magnitude) < float.Epsilon)
        //&& !controllable && gameObject.tag == "Parent for Player(Clone)")
        {
            Debug.Log("oh no!");
        }
        transform.position = vector;
    }

}
