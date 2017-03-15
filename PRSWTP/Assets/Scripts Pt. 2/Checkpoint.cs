using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
   /* public float resetX;
    public float resetY;
    public float resetZ;
    */

    public float targetX;
    public float targetY;
    public float targetZ;

    // Use this for initialization
    void Start () {
//        resetX = <GetComponent>() resetX;
//        resetY = <GetComponent>() resetY;
//        resetZ = <GetComponent>() resetZ;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 returnSpawnLocation ()
    {
        return new Vector3(targetX, targetY, targetZ);
    }
}
