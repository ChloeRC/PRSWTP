using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour {

    //private bool direction;
    private Quaternion rightRot = Quaternion.Euler(0, 270, 0);
    private Quaternion leftRot = Quaternion.Euler(0, 90, 0);

	// Use this for initialization
	void Start () {
        //direction = playerScript.DIR_RIGHT;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Rotate(bool rotation)
    {
        if (rotation == PlayerScript.DIR_RIGHT)
        {
            transform.rotation = leftRot;
        }
        else if (rotation == PlayerScript.DIR_LEFT)
        {
            transform.rotation = rightRot;
        }
    }
}
