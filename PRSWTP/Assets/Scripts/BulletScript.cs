using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float speed;

    public bool dir;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (dir == false)
        {
            rb.AddForce(Vector3.right * speed);
        }
        else
        {
            rb.AddForce(Vector3.right * speed);
        }
    }
}
