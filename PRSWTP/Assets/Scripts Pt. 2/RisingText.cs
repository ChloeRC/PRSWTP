using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingText : MonoBehaviour {

    public bool isRising;

	// Use this for initialization
	void Start () {
        isRising = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRising)
        {
            Vector3 pos = transform.position; 
            transform.position = new Vector3(pos.x, pos.y + Time.deltaTime * 1.5f, pos.z);
        }
	}
}
