using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {
    public Rigidbody rb; 

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	
	void FixedUpdate () {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0.0f, z);
        rb.AddForce(movement);
	}
}
