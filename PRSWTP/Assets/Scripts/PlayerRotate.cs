using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Rotate(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(rotation);
    }
}
